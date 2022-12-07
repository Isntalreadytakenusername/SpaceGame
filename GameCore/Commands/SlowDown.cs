using GameCore.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Commands
{
    public class SlowDown: ICommandForSpell
    {
        public int Price { get { return price; } }
        private int price;

        public SlowDown(int price)
        {
            this.price = price;
        }

        public void Execute(ICharacter target)
        {
            target.SetSpeed((target.GetSpeed() / 2));
            //target.SetSpeedStrategy(new ModifiedSpeedStrategy());
        }


        public int GetPrice()
        {
            return this.price;
        }

        public string GetSpellName()
        {
            return "SlowDown";
        }
    }
}
