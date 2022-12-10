using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCore.Commands;
using Merlin2d.Game;
using Merlin2d.Game.Actions;
namespace GameCore.Spells
{
    public class SelfCastSpellBuilder : ISpellBuilder
    {
        private SelfCastSpell spellThatIsBeingBuilt;
        private SpellInfo spellInfo;
        private IWizard SpellCaster;
        private List<ICommandForSpell> SpellEffects;
        private int spellPrice;

        
        public SelfCastSpellBuilder(SpellInfo spellInfo, int spellPrice, IWizard SpellCaster)
        {
            this.spellInfo = spellInfo;
            this.SpellCaster = SpellCaster;
            this.spellPrice = spellPrice;
            this.spellThatIsBeingBuilt = new SelfCastSpell(SpellCaster);
        }

        public ISpellBuilder AddEffect(string effectName)
        {
            if (string.Compare(effectName, "SpeedUp") == 0)
            {
                this.spellThatIsBeingBuilt.AddEffect(new SpeedUp(this.spellPrice));
            }
            else if (string.Compare(effectName, "Heal") == 0)
            {
                this.spellThatIsBeingBuilt.AddEffect(new Heal(this.spellPrice, 20));
            }
            return this;
        }


        public ISpell GetSpell()
        {
            return this.spellThatIsBeingBuilt;
        }

        public ISpellBuilder SetAnimation(Animation animation)
        {
            // do nothing
            return this;
        }
        public ISpellBuilder SetSpellCost(int cost)
        {
            throw new NotImplementedException();// this makes more sense to be in constructor, a spell can not be without price; NotNeededException()
        }
    }
}
