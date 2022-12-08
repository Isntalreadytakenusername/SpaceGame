using GameCore.Commands;
using Merlin2d.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Actors
{
    internal class Explosion : AbstractActor
    {
        private int existenceCount = 30;
        public Explosion(int x, int y, string size): base("Explosion")
        {

            if (size == "big")
            {
                SetPosition(x - 100, y - 200);
                SetAnimation(new Animation("resources/sprites/big_explosion.png", 512, 512));
            }
            else if (size == "small")
            {
                SetPosition(x, y);
                SetAnimation(new Animation("resources/sprites/explosion2.png", 181, 181));
            }
            this.GetAnimation().Start();
        }
        
        
        public override void Update()
        {
            existenceCount--;
            if (existenceCount == 0)
            {
                this.GetWorld().RemoveActor(this);
            }
        }
    }
}
