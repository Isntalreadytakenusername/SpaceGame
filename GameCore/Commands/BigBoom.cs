using GameCore.Actors;
using Merlin2d.Game.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Commands
{
    internal class BigBoom: ICommandForSpell
    {
        public void Execute(ICharacter target)
        {
            // first get coordinates of the target, then get all characters in the area, call ChangeHealth on them
            int x = target.GetX();
            int y = target.GetY();
            int radius = 230;
            List<IActor> actors = target.GetWorld().GetActors();
            
            foreach (IActor actor in actors)
            {
                if (actor is ICharacter)
                {
                    ICharacter character = (ICharacter)actor;
                    int x1 = character.GetX();
                    int y1 = character.GetY();
                    if (Math.Sqrt(Math.Pow(x1 - x, 2) + Math.Pow(y1 - y, 2)) <= radius)
                    {
                        character.ChangeHealth(-100);
                    }
                }
            }

            //target.Health -= 100;
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
