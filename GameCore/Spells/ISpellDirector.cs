using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Merlin2d.Game.Actions;

namespace GameCore.Spells
{
    public interface ISpellDirector
    {
        ISpell Build(string spellName);
    }
}
