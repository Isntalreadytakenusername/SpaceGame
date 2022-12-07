using GameCore.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merlin2d.Game.Actions;
using GameCore.Commands;

namespace GameCore.Spells
{
    public interface ISpell
    {
        void AddEffect(ICommandForSpell effect);
        //void AddEffects(IEnumerable<ICommand> effects);
        int GetCost();
        void ApplyEffects(ICharacter target);
    }
}
