using GameCore.Actors;
using Merlin2d.Game.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Commands
{
    public class Damage: ICommandForSpell
    {
        private int price;
        private int damage;

        public Damage(int price, int damage)
        {
            this.damage = damage;
            this.price = price;
        }

        public void Execute(ICharacter target)
        {
            target.ChangeHealth(-damage);
        }

        public int GetPrice() 
        {
            return this.price;
        }

        public string GetSpellName()
        {
            return "Damage";
        }
    }
}
