using GameCore.Commands;
using GameCore.Items;
using GameCore.Spells;
using Merlin2d.Game;
using Merlin2d.Game.Actions;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Actors
{
    public class PlayerWizard: AbstractWizard
    {
        private Animation animation;
        private ICommand lastMove;
        private ICommand moveUp;
        private ICommand moveDown;
        private ICommand moveRight;
        private ICommand moveLeft;
        private Random rand = new Random();
        private int EnemySpawnCoolDown = 0;
        private int EnemyCargoSpawnCoolDown = 0;

        private int howManyRebelsAreLetThrough = 0;
        private bool isCargoShipMissed = false;
        private int howManyRebelsWereSpawned = 0;
        private int healingKitCoolDown = 100;
        private int bigBoomCoolDown = 120;
        private int collisionCoolDown = 0;
        private int freezingUnitCoolDown = 300;

        private int bigBoomsCount = 10;
        private int freezingUnitsCount = 1;
        
        private int destroyedCargoShipsCount = 0;

        private int genericCounter = 0;
        private bool isAddedMessage = false;

        private IMessage healthMessage;
        private IMessage introInfo;
        private bool isIntroRemoved = false;
        public PlayerWizard(int x, int y, string name, double speed, int health, ISpeedStrategy speedStrategy, int energy) : base(name, speed, health, speedStrategy, energy )
        {
            
            this.SetPosition(x, y);

            animation = new Animation("resources/sprites/player.png", 70, 69);
            this.SetAnimation(animation);
            this.GetAnimation().Start();

            this.speed = speed;
            this.moveUp = new Move(this, speed, 0, -1);
            this.moveDown = new Move(this, speed, 0, 1);
            this.moveRight = new Move(this, speed, 1, 0);
            this.moveLeft = new Move(this, speed, -1, 0);
            this.lastMove = moveRight;

            this.healthMessage = new Message("Health: " + this.health, 0, 0, 30, Color.White, MessageDuration.Indefinite);
            //this.healthMessage.SetAnchorPoint(this);
            this.introInfo = new Message("You are taking part in a blockade of a planet. Your task is not to let the convoy of \n the enemy ships through. You need to destroy 3 cargo ships of the enemy." +
                " \n And we cannot let any one of them to pass through to resupply the forces on the planet. \n" +
                "To shoot press SPACE, to shoot with a big bomb press X, \n to freeze the enemy press S. \n" +
                "You can pick items with E. To close the message press SPACE \n", 25, 300, 20, Color.White, MessageDuration.Indefinite);

            this.EnemyCargoSpawnCoolDown = rand.Next(0, 10);
        }

        public override int GetLastDirection() 
        {
            if (lastMove == moveLeft)
                return 1;
            return 0;
        }

        public void SpawnEnemies()
        {
            this.howManyRebelsWereSpawned++;
            this.GetWorld().AddActor(new Enemy(700, rand.Next(0, 800), "Enemy2", 1, 15, new NormalSpeedStrategy(), 1000, this));
        }

        public void SpawnCargoShip()
        {
            this.GetWorld().AddActor(new EnemyCargoShip(610, rand.Next(100, 600), "CargoShip", 0.2, 1000, new ModifiedSpeedStrategy(), 500, this));
        }

        public void MissedRebel() 
        {
            this.howManyRebelsAreLetThrough++;
        }

        public void MissedCargoShip()
        {
            this.isCargoShipMissed = true;
        }

        public void DestroyedCargoShip()
        {
            this.destroyedCargoShipsCount++;
        }
        
        public int GetDestroyedCargoShipsCount() 
        {
            return this.destroyedCargoShipsCount;
        }

        public bool IsCargoShipMissed() 
        {
            return this.isCargoShipMissed;
        }

        public static MapStatus CheckForDefeatOrVictory(IWorld world)
        {
            List<IActor> Players = world.GetActors();
            PlayerWizard player = null;
            foreach (IActor actor in Players) 
            {
                if(actor.GetName() == "Player")    
                    player = (PlayerWizard)actor;
            }
            
            if(player == null)
                return MapStatus.Failed;

            if (player.howManyRebelsAreLetThrough >= 10)
                return MapStatus.Failed;
            else if (player.GetDestroyedCargoShipsCount() >= 3)
                return MapStatus.Finished;
            else if(player.RemovedFromWorld() || player.GetHealth() <= 0 || player.IsCargoShipMissed())
            {
                Animation animation = new Animation("resources/sprites/explosion2.png", 181, 181);
                player.SetAnimation(animation);
                player.GetAnimation().Start();
                Thread.Sleep(2000);
                return MapStatus.Failed;
            }
            else
                return MapStatus.Unfinished;
        }


        private void GenerateHealingKits() 
        {
            if (this.healingKitCoolDown == 0)
            {
                this.GetWorld().AddActor(new HealingKit(rand.Next(0, 700), rand.Next(0, 860), "HealingKit"));
                this.healingKitCoolDown = rand.Next(500, 1000);
            }
            else
                this.healingKitCoolDown--;
        }

        private void GenerateBigBooms() 
        {
            if (this.bigBoomCoolDown == 0)
            {
                this.GetWorld().AddActor(new BigBoomUnit(rand.Next(0, 700), rand.Next(0, 860), "BigBoom"));
                this.bigBoomCoolDown = rand.Next(350, 800);
            }
            else
                this.bigBoomCoolDown--;
        }

        private void GenerateFreezingUnits()
        {
            if (this.freezingUnitCoolDown == 0)
            {
                this.GetWorld().AddActor(new FreezingUnit(rand.Next(0, 700), rand.Next(0, 860), "FreezingUnit"));
                this.freezingUnitCoolDown = rand.Next(1000, 2000);
            }
            else
                this.freezingUnitCoolDown--;
        }

        public void LookForUsables() 
        {
            // get coordinates of the player, get all the players from world
            // check if the distance between the player and any IUsable actor is less than 50
            // if so call Use on IUsable
            List<IActor> actors = this.GetWorld().GetActors();
            foreach (IActor actor in actors)
            {
                if (actor is IUsable)
                {
                    IUsable usable = (IUsable)actor;
                    double distance = Math.Sqrt(Math.Pow(this.GetX() - actor.GetX(), 2) + Math.Pow(this.GetY() - actor.GetY(), 2));
                    if (distance < 50)
                    {
                        usable.Use(this);
                    }
                }
            }
        }

        public void CheckForCollisions() 
        {
            List<IActor> actors = this.GetWorld().GetActors();
            foreach (IActor actor in actors)
            {
                if (actor is Enemy)
                {
                    double distance = Math.Sqrt(Math.Pow(this.GetX() - actor.GetX(), 2) + Math.Pow(this.GetY() - actor.GetY(), 2));
                    if (distance < 40 && this.collisionCoolDown <= 0)
                    {
                        this.GetWorld().AddActor(new Explosion(GetX(), GetY(), "big"));
                        ((Enemy)actor).Die();
                        this.ChangeHealth(-100);
                        this.collisionCoolDown = 45;
                    }
                }
            }
        }
         

        public override void Cast(string spellName)
        {
            this.spellToBeUsedSoonSomehowInUnknownManner = new SpellDirector(this).Build(spellName);

            if (this.spellToBeUsedSoonSomehowInUnknownManner.GetCost() <= this.energy && this.spellCoolDownTime >= 4)
            {
                this.energy -= this.spellToBeUsedSoonSomehowInUnknownManner.GetCost();
                this.GetWorld().AddActor((IActor)this.spellToBeUsedSoonSomehowInUnknownManner);
                this.spellCoolDownTime = 0;
            }
        }

        public void PickBigBoom() 
        {
            this.bigBoomsCount++;
        }

        public void PickFreezingUnit() 
        {
            this.freezingUnitsCount++;
        }



        public override void Update()
        {
            if (!isAddedMessage)
            {
                isAddedMessage = true;
                this.GetWorld().AddMessage(this.healthMessage);
                this.GetWorld().AddMessage(this.introInfo);
            }
            if (!isIntroRemoved)
            {
                if (Input.GetInstance().IsKeyDown(Input.Key.SPACE))
                {
                    this.GetWorld().RemoveMessage(introInfo);
                    isIntroRemoved = true;
                }
            }
            else 
            {
                this.healthMessage.SetText("Health: " + this.health + " B: " + this.bigBoomsCount + " F: " + this.freezingUnitsCount + " " + "CARGO: " + this.destroyedCargoShipsCount + "/3" + " Missed: " + this.howManyRebelsAreLetThrough + "/10");

                this.DieCountdownCheck();
                this.spellCoolDownTime++;
                this.EnemySpawnCoolDown--;
                this.collisionCoolDown--;  // we may decrement every time without checking the value
                this.EnemyCargoSpawnCoolDown--;
                Console.WriteLine(this.health);

                GenerateHealingKits();
                GenerateBigBooms();
                GenerateFreezingUnits();

                CheckForCollisions();




                if (this.EnemySpawnCoolDown <= 0)
                {
                    this.SpawnEnemies();
                    this.EnemySpawnCoolDown = rand.Next(0, 85);
                }
                if (this.EnemyCargoSpawnCoolDown <= 0)
                {
                    this.SpawnCargoShip();
                    this.EnemyCargoSpawnCoolDown = rand.Next(1500, 2500);
                }
                if (Input.GetInstance().IsKeyDown(Input.Key.UP))
                {
                    animation.Start();
                    this.moveUp.Execute();
                }
                else if (Input.GetInstance().IsKeyDown(Input.Key.DOWN))
                {
                    animation.Start();
                    this.moveDown.Execute();
                }
                else if (Input.GetInstance().IsKeyDown(Input.Key.RIGHT))
                {
                    if (this.lastMove == moveLeft)
                        animation.FlipAnimation();
                    animation.Start();
                    this.moveRight.Execute();
                    this.lastMove = moveRight;
                }
                else if (Input.GetInstance().IsKeyDown(Input.Key.LEFT))
                {
                    if (this.lastMove == moveRight)
                        animation.FlipAnimation();
                    animation.Start();
                    this.moveLeft.Execute();
                    this.lastMove = moveLeft;
                }
                else if (Input.GetInstance().IsKeyDown(Input.Key.SPACE))
                {
                    this.Cast("Damage");
                }
                else if (Input.GetInstance().IsKeyDown(Input.Key.S))
                {
                    if (this.freezingUnitsCount > 0 && this.spellCoolDownTime >= 50)
                    {
                        this.Cast("SlowDown");
                        this.spellCoolDownTime = 0;
                        this.freezingUnitsCount--;
                    }
                }
                else if (Input.GetInstance().IsKeyDown(Input.Key.X))
                {
                    if (this.bigBoomsCount > 0 && this.spellCoolDownTime >= 50)  //spell cool down needs to be checked not to spend more units than casted
                    {
                        this.Cast("BigBoom");
                        this.spellCoolDownTime = 0;
                        this.bigBoomsCount--;
                    }
                }
                else if (Input.GetInstance().IsKeyDown(Input.Key.E))
                {
                    LookForUsables();
                }
                else
                {
                    animation.Stop();
                }
            }
        }
            
    }
}
