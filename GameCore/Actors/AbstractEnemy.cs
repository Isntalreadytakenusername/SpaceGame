using GameCore.Commands;
using GameCore.Spells;
using Merlin2d.Game;
using Merlin2d.Game.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Actors
{
    public abstract class AbstractEnemy: AbstractWizard, ICharacter
    {
        protected Animation animation;
        protected ICommand lastMove;
        protected ICommand moveUp;
        protected ICommand moveDown;
        protected ICommand moveRight;
        protected ICommand moveLeft;
        protected PlayerWizard protagonist;
        protected double old_speed;
        protected Random random = new Random();

        protected bool is_dead = false; // this I create specifically to tackle the problem that projectiles continue to hit explosion.

        public AbstractEnemy(int x, int y, string name, double speed, int health, ISpeedStrategy speedStrategy, int energy, PlayerWizard protagonist) : base(name, speed, health, speedStrategy, energy)
        {
            this.SetPosition(x, y);

            this.speed = speed;
            this.old_speed = speed;
            this.moveUp = new Move(this, speed, 0, -1);
            this.moveDown = new Move(this, speed, 0, 1);
            this.moveRight = new Move(this, speed, 1, 0);
            this.moveLeft = new Move(this, speed, -1, 0);
            this.lastMove = moveLeft;

            this.protagonist = protagonist;
        }

        public override int GetLastDirection()
        {
            return 1;
        }

        public bool IsDead()
        {
            return this.is_dead;
        }

        public override void Cast(string spellName)
        {
            this.spellToBeUsedSoonSomehowInUnknownManner = new SpellDirector(this).Build(spellName);

            if (this.spellToBeUsedSoonSomehowInUnknownManner.GetCost() <= this.energy && this.spellCoolDownTime >= random.Next(20,60))
            {
                this.energy -= this.spellToBeUsedSoonSomehowInUnknownManner.GetCost();
                this.GetWorld().AddActor((Merlin2d.Game.Actors.IActor)this.spellToBeUsedSoonSomehowInUnknownManner);
                this.spellCoolDownTime = 0;
            }
        }

        protected virtual void PassedThroughBlockade()
        {
            if (this.GetX() < 10)
            {
                this.protagonist.MissedRebel();
                this.GetWorld().RemoveActor(this);
            }
        }

        protected void RecreateMove()
        {
            this.moveUp = new Move(this, speed, 0, -1);
            this.moveDown = new Move(this, speed, 0, 1);
            this.moveRight = new Move(this, speed, 1, 0);
            this.moveLeft = new Move(this, speed, -1, 0);
        }



        public override void Update()
        {
            DieCountdownCheck();
            this.PassedThroughBlockade();
            this.moveLeft.Execute();
            this.spellCoolDownTime++;
            this.Cast("Damage");

            if (old_speed != speed)
            {
                RecreateMove();
                old_speed = speed;
            }
        }
    }
}
