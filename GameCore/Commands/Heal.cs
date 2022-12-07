using GameCore.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Commands
{
    public class Heal: ICommandForSpell
    {
        public int Price { get { return price; } }
        private int price;
        private int healAmount;
        
        public Heal(int price, int healAmount)
        {
            this.healAmount = healAmount;
            this.price = price;
        }

        public void Execute(ICharacter target)
        {
            target.ChangeHealth(healAmount);
        }


        public int GetPrice()
        {
            return this.price;
        }

        public string GetSpellName()
        {
            return "Heal";
        }
    }
}
