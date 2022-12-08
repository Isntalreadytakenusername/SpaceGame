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
    internal class FreezingUnit: AbstractPickable
    {
        public FreezingUnit(int x, int y, string name) : base(x, y, name)
        {
            SetAnimation(new Animation("resources/sprites/freezing_unit.png", 35, 38));
            this.GetAnimation().Start();
        }


        public override void Use(IActor actor)
        {
            ((PlayerWizard)actor).PickFreezingUnit();
            this.isUsed = true;
        }
    }
}
