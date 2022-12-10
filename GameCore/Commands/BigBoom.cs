using GameCore.Actors;
using Merlin2d.Game.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Commands
{
    internal class BigBoom : ICommandForSpell
    {
        public void Execute(ICharacter target)
        {
            int radius = 300;
            List<IActor> actors = target.GetWorld().GetActors();

            foreach (IActor actor in actors)
            {
                if (actor is ICharacter && ((ICharacter)actor != target))
                {
                    ICharacter character = (ICharacter)actor;
                    
                    double distance = Math.Sqrt(Math.Pow(character.GetX() - target.GetX(), 2) + Math.Pow(character.GetY() - target.GetY(), 2));
                    
                    if (distance <= radius)
                    {
                        character.ChangeHealth(-100);
                    }
                }
            }
            target.ChangeHealth(-100);

        }

        public int GetPrice()
        {
            return 10;
        }

        public string GetSpellName()
        {
            return "BigBoom";
        }
    }
}
