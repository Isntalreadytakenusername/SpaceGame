using GameCore.Actors;
using Merlin2d.Game.Actors;
using Merlin2d.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Items
{
    public abstract class AbstractPickable: AbstractActor, IUsable
    {
        protected bool isUsed = false;
        public AbstractPickable(int x, int y, string name) : base(name)
        {
            SetPosition(x, y);
        }



        public override void Update()
        {
            if (isUsed)
            {
                this.RemoveFromWorld();
            }
        }

        public virtual void Use(IActor actor)
        {
            this.isUsed = true;
        }
    }
}

