using GameCore.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Merlin2d.Game.Actions;
using GameCore.Commands;
using Merlin2d.Game.Actors;
using Merlin2d.Game;
using GameCore.Items;

namespace GameCore.Spells
{
    public class ProjectileSpell : AbstractActor, IMovable, ISpell
    {
        private IWizard SpellCaster;
        private List<ICommandForSpell> SpellEffects = new List<ICommandForSpell>();

        private ISpeedStrategy speedStrategy;
        private SpellType SpellType = SpellType.ProjectileSpell;

        private int speed = 30;
        private int lastDirection;

        private Move moveRight;
        private Move moveLeft;

        private List<IActor> existingActors;

        private double oldX;
        private double oldY;


        public ProjectileSpell(IWizard SpellCaster)
        {
            this.SpellCaster = SpellCaster;
            //this.lastDirection = SpellCaster.GetActorOrientation();
            this.moveRight = new Move(this, speed, 1, 0);
            this.moveLeft = new Move(this, speed, -1, 0);
            this.SetPosition(SpellCaster.GetX(), SpellCaster.GetY());
            this.lastDirection = this.SpellCaster.GetLastDirection();
        }
        
        public void AddEffect(ICommandForSpell effect)
        {
            this.SpellEffects.Add(effect);
        }

        public void ApplyEffects(ICharacter target)
        {
            foreach (ICommandForSpell SpellEffect in SpellEffects) 
            {
                SpellEffect.Execute(target);
            }
            
        }

        public int GetCost() 
        {
            int sum = 0;
            foreach (ICommandForSpell SpellEffect in SpellEffects)
            {
                sum += SpellEffect.GetPrice();
            }
            return sum;
        }

        public double GetSpeed(double speed)
        {
            return speed;
        }

        public double GetSpeed()
        {
            return speed;
        }

        public void SetSpeedStrategy(ISpeedStrategy strategy)
        {
            this.speedStrategy = strategy;
        }

        private bool IntersectWithWallAlternative() 
        {
            // of coordinates of the spell not changing than it hit something
            if (oldX == this.GetX() && oldY == this.GetY())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IntersectsWithAnything()
        {
            this.existingActors = GetWorld().GetActors();
            foreach (IActor actor in existingActors)
            {
                if (((this.IntersectsWithActor(actor) && (actor != this) && (actor != this.SpellCaster) && (this.SpellCaster.GetName() == "Player")) || ((this.IntersectsWithActor(actor) && (actor != this) && (actor != this.SpellCaster) && (this.SpellCaster.GetName() == "Enemy2") && (actor.GetName() != "Enemy2")) )) && (actor is not IUsable))
                {
                    try
                    {
                        //((ICharacter)actor).ChangeHealth(-10);
                        bool isBigBoom = false;
                        // I dont use multiple spell effects. Here I just check if it was a big boom spell
                        // to generate an Explosion in this case
                        foreach (ICommandForSpell SpellEffect in SpellEffects)
                        {
                           SpellEffect.Execute((ICharacter)actor);
                           if(SpellEffect.GetSpellName()=="BigBoom")
                                isBigBoom = true;
                        }
                        if (((ICharacter)actor).GetHealth() <= 0)
                        {

                            if (isBigBoom)
                                // big explosion is made as a separate actor because replacing animation of the target with explosion
                                // resulted in explosion being out of place dramatically
                                this.GetWorld().AddActor(new Explosion(actor.GetX(), actor.GetY()));
                            else
                            {
                                if (actor.GetName() == "Player")
                                    this.GetWorld().AddActor(new Explosion(actor.GetX(), actor.GetY()));
                                else
                                {
                                    actor.SetAnimation(new Animation("resources/sprites/explosion2.png", 181, 181));
                                    actor.GetAnimation().Start();
                                }
                            }
                            ((AbstractCharacter)actor).Die();
                        }
                    }
                    catch 
                    {
                        return true;
                    }
                    return true;
                }
            }
            return this.IntersectWithWallAlternative();
            //return (this.GetWorld().GetActors().Any(actor => actor != this && this.IntersectsWithActor(actor))) || this.GetWorld().IntersectWithWall(this);
        }

        public override void Update()
        {
            
            if (this.lastDirection == 1) 
            {
                this.moveLeft.Execute();
                // somehow I need a list of all actors to check intersection, and do not forget to put the caster as exception
                // something like this.GetWorld().GetActors() to get a list of all actors
            }
            else
            {
                this.moveRight.Execute();
                // somehow I need a list of all actors to check intersection, and do not forget to put the caster as exception
            }

            //Console.WriteLine(this.GetWorld().IntersectWithWall(this));
            if (this.IntersectsWithAnything())
            {
                this.GetWorld().RemoveActor(this);
            }
            this.oldX = this.GetX();
            this.oldY = this.GetY();
        }
    }
}
