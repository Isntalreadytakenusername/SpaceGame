using Merlin2d.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Merlin2d.Game.Actions;

namespace GameCore.Spells
{
    public interface ISpellBuilder
    {
        ISpellBuilder AddEffect(string effectName);
        ISpellBuilder SetAnimation(Animation animation); //unused for self-cast spells
        ISpellBuilder SetSpellCost(int cost);
        
        ISpell GetSpell();
    }
}
