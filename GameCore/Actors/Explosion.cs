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
        public Explosion(int x, int y)
        {
            SetPosition(x-100,y-200);
            SetAnimation(new Animation("resources/sprites/big_explosion.png", 512, 512));
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
