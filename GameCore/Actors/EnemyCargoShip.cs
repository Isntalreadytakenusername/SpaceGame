using Merlin2d.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Actors
{
    internal class EnemyCargoShip: Enemy
    {
        public EnemyCargoShip(int x, int y, string name, double speed, int health, ISpeedStrategy speedStrategy, int energy, PlayerWizard protagonist) : base(x, y, name, speed, health, speedStrategy, energy, protagonist)
        {
            SetAnimation(new Animation("resources/sprites/enemy_cargo_ship.png", 336, 230));
            this.GetAnimation().Start();
        }

        protected override void PassedThroughBlockade()
        {
            if (this.GetX() < 10)
            {
                this.protagonist.MissedRebel();
                this.GetWorld().RemoveActor(this);
            }
        }
    }
}
