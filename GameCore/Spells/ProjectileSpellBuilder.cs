using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCore.Actors;
using GameCore.Commands;
using Merlin2d.Game;
using Merlin2d.Game.Actions;

namespace GameCore.Spells
{
    public class ProjectileSpellBuilder: ISpellBuilder
    {
        private ProjectileSpell spellThatIsBeingBuilt;
        private SpellInfo spellInfo;
        private IWizard SpellCaster;
        private List<ICommandForSpell> SpellEffects;
        private int spellPrice;


        public ProjectileSpellBuilder(SpellInfo spellInfo, int spellPrice, IWizard SpellCaster)
        {
            this.spellInfo = spellInfo;
            this.SpellCaster = SpellCaster;
            this.spellPrice = spellPrice;
            this.spellThatIsBeingBuilt = new ProjectileSpell(SpellCaster);
        }

        public ISpellBuilder AddEffect(string effectName)
        {
            if (string.Compare(effectName, "Damage") == 0) 
            {
                this.spellThatIsBeingBuilt.AddEffect(new Damage(this.spellPrice, 10));
            }
            else if (string.Compare(effectName, "SlowDown") == 0)
            {
                this.spellThatIsBeingBuilt.AddEffect(new SlowDown(this.spellPrice));
            }
            else if (string.Compare(effectName, "BigBoom") == 0)
            {
                this.spellThatIsBeingBuilt.AddEffect(new BigBoom());
            }
            return this;
        }


        public ISpell GetSpell()
        {
            return this.spellThatIsBeingBuilt;
        }

        public ISpellBuilder SetAnimation(Animation animation)
        {
            this.spellThatIsBeingBuilt.SetAnimation(animation);
            return this;
        }
        public ISpellBuilder SetSpellCost(int cost)
        {
            throw new NotImplementedException();// this makes more sense to be in constructor, a spell can not be without price; NotNeededException()
        }


    }
}
