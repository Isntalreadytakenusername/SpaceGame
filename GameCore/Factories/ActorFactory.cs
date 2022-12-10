using GameCore.Actors;
using Merlin2d.Game;
using Merlin2d.Game.Actors;

namespace GameCore.Factories
{
    public class ActorFactory : IFactory
    {
        public IActor Create(string actorType, string actorName, int x, int y)
        {
            IActor actor = null;

            if (actorType == "Player")
            {
                actor = new PlayerWizard(x, y, actorName, 2, 100, new NormalSpeedStrategy(), 10000);
                actor.SetName("Player");
                //actor.SetPhysics(true);
                actor.SetPosition(x, y);
            }
            else
            {
                Console.WriteLine(actorType);
                Console.WriteLine(actorName);
                Console.WriteLine(x);
                Console.WriteLine(y);
            }

            return actor;
        }
    }
}
