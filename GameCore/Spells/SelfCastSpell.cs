using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCore.Actors;
using GameCore.Commands;
using Merlin2d.Game.Actions;

namespace GameCore.Spells
{
    public class SelfCastSpell: ISpell
    {
        IWizard SpellCaster;
        List<ICommandForSpell> SpellEffects;
        
        SpellType SpellType = SpellType.SelfCastSpell;

        public SelfCastSpell(IWizard SpellCaster)
        {
            this.SpellCaster = SpellCaster;
        }

        public void AddEffect(ICommandForSpell effect)
        {
            this.SpellEffects.Add(effect);
        }

        public void ApplyEffects(ICharacter target)
        {
            foreach (ICommandForSpell SpellEffect in SpellEffects)
            {
                SpellEffect.Execute(target);
            }

        }
        public int GetCost()
        {
            int sum = 0;
            foreach (ICommandForSpell SpellEffect in SpellEffects)
            {
                sum += SpellEffect.GetPrice();
            }
            return sum;
        }
    }
}
