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
    public class Enemy : AbstractEnemy, ICharacter
    {      
        public Enemy(int x, int y, string name, double speed, int health, ISpeedStrategy speedStrategy, int energy, PlayerWizard protagonist) : base(x, y, name, speed, health, speedStrategy, energy, protagonist)
        {
            animation = new Animation("resources/sprites/enemy.png", 70, 62);
            this.SetAnimation(animation);
            this.GetAnimation().Start();
        }
    }
}
