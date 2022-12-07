using Merlin2d.Game.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Merlin2d.Game.Actions;

namespace GameCore.Spells
{
    public interface IWizard : IActor
    {
        void ChangeMana(int delta);
        int GetMana();

        int GetLastDirection();
        public void Cast(string spellName);
        public void UseSpell();
    }
}
