using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCore.Actors;
using GameCore.Commands;
using GameCore.Factories;
using Merlin2d.Game;
using Merlin2d.Game.Actors;
using System.Runtime.InteropServices;
using Merlin2d.Game.Actions;

namespace GameCore.Actors
{
    public abstract class AbstractCharacter: AbstractActor, ICharacter
    {
        protected enum ActorOrientation { FacingLeft, FacingRight };
        protected ActorOrientation orientation;
        protected int health;
        protected List<ICommand> effects;
        protected ISpeedStrategy speedStrategy;
        protected double speed;

        protected int countdownToDisappear = 0;

        public AbstractCharacter(string name, double speed, int health, ISpeedStrategy speedStrategy) : base(name)
        {
            this.health = health;
            this.speedStrategy = speedStrategy;
            effects = new List<ICommand>();
            this.speed = speed;
        }


        public void ChangeHealth(int delta) 
        {
           
            this.health += delta;
            if (health <= 0)
            {
                /*Animation animation = new Animation("resources/sprites/explosion2.png", 181, 181);
                this.SetAnimation(animation);
                this.GetAnimation().Start();*/
                Die();
            }
        }
        public int GetHealth()
        {
            return health;
        }
        public virtual void Die() 
        {
            this.countdownToDisappear = 20;
            //this.RemoveFromWorld();
        }

        public void DieCountdownCheck() 
        {
            if (this.countdownToDisappear != 0) 
            {
                this.countdownToDisappear--;
                if (this.countdownToDisappear == 0)
                    this.RemoveFromWorld();
            }
        }

        public void SetSpeedStrategy(ISpeedStrategy strategy) 
        {
            this.speedStrategy = strategy;
        }

        public void AddEffect(ICommand effect) 
        {
            this.effects.Add(effect);
        }
        public void RemoveEffect(ICommand effect) 
        {
            this.effects.Remove(effect);
        }

/*        public double GetSpeed(double speed) 
        {
            return this.speedStrategy.GetSpeed(speed);
        }*/

        public double GetSpeed() 
        {
            return this.speed;
        }

        public void SetSpeed(double speed)
        {
            this.speed = speed;
        }

        public int GetActorOrientation() 
        {
            return (int)this.orientation;
        }


    }
}
