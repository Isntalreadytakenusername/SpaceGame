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
    public class BigBoomUnit: AbstractPickable
    {
        public BigBoomUnit(int x, int y, string name) : base(x, y, name)
        {
            SetAnimation(new Animation("resources/sprites/big_boom_unit.png", 35, 26));
            this.GetAnimation().Start();
        }


        public override void Use(IActor actor)
        {
            ((PlayerWizard)actor).PickBigBoom();
            this.isUsed = true;
        }
    }

}

