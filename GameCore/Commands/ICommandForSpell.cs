using GameCore.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Commands
{
    public interface ICommandForSpell
    {
        void Execute(ICharacter target);
        int GetPrice();
        string GetSpellName();
    }
}
