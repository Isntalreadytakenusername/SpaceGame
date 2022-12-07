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
    internal class HealingKit: AbstractActor, IUsable
    {
        private bool isUsed = false;
        public HealingKit(int x, int y, string name) : base(name)
        {
            // put healing png// make the character be able to press button to use any item withing its range, you can use "is IUsable" to check for the type of an actor nearby
            // than make this healing stuff appear randomly on the map
            SetPosition(x, y);
            SetAnimation(new Animation("resources/sprites/rsz_heal.png", 35, 35));
            this.GetAnimation().Start();
        }



        public override void Update()
        {
            if (isUsed)
            {
                this.RemoveFromWorld();
                this.GetWorld().RemoveActor(this);
                //this.Die();
                this.SetAnimation(new Animation("resources/sprites/kill_healing_kit_finally.png", 1, 1));
            }
        }

        public void Use(IActor actor)
        {
            ((PlayerWizard)actor).ChangeHealth(100);
            this.isUsed = true;
        }
    }
}
