using Merlin2d.Game;
using Merlin2d.Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Actors
{
    internal class EnemyCargoShip: AbstractEnemy, ICharacter
    {
        private IMessage healthMessage;
        public EnemyCargoShip(int x, int y, string name, double speed, int health, ISpeedStrategy speedStrategy, int energy, PlayerWizard protagonist) : base(x, y, name, speed, health, speedStrategy, energy, protagonist)
        {
            animation = new Animation("resources/sprites/enemy_cargo_ship.png", 336, 230);
            this.SetAnimation(animation);
            this.GetAnimation().Start();

            this.healthMessage = new Message("Health: " + this.health, 300, 300, 10, Color.White, MessageDuration.Indefinite);
            this.healthMessage.SetAnchorPoint(this);
        }

        public override void Die() 
        {
            this.is_dead = true;
            this.countdownToDisappear = 20;
            this.protagonist.DestroyedCargoShip();
        }

        protected override void PassedThroughBlockade()
        {
            if (this.GetX() < 10)
            {
                this.protagonist.MissedCargoShip();
                this.GetWorld().RemoveActor(this);
            }
        }

        public override void Update()
        {
            this.healthMessage.SetText("Health: " + this.health);

            DieCountdownCheck();
            this.PassedThroughBlockade();
            this.moveLeft.Execute();
            this.spellCoolDownTime++;
            this.Cast("BigBoom");

            if (old_speed != speed)
            {
                RecreateMove();
                old_speed = speed;
            }
        }
    }
}
