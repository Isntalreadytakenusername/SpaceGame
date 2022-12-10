using GameCore.Actors;
using Merlin2d.Game;
using Merlin2d.Game.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Items
{
    internal class HealingKit: AbstractPickable
    {
        
        public HealingKit(int x, int y, string name) : base(x, y, name)
        {
            SetAnimation(new Animation("resources/sprites/rsz_heal.png", 35, 35));
            this.GetAnimation().Start();
        }


        public override void Use(IActor actor)
        {
            ((PlayerWizard)actor).ChangeHealth(500);
            this.isUsed = true;
        }
    }
}
