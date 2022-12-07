using GameCore.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Commands
{
    public class SpeedUp: ICommandForSpell
    {
        public int Price { get { return price; } }
        private int price;// 70% modifier is better to be put when mana is subtracted in case self-cast occurs

        public SpeedUp(int price)
        {
            this.price = price;
        }

        public void Execute(ICharacter target)
        {
            target.SetSpeedStrategy(new NormalSpeedStrategy());
        }


        public int GetPrice()
        {
            return this.price;
        }

        public string GetSpellName()
        {
            return "SpeedUp";
        }
    }
}
