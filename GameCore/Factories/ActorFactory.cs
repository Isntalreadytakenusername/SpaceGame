using GameCore.Actors;
using Merlin2d.Game;
using Merlin2d.Game.Actors;

namespace GameCore.Factories
{
    public class ActorFactory : IFactory
    {
        public IActor Create(string actorType, string actorName, int x, int y)
        {
            return new PlayerWizard(100, 100, "Player", 10, 3000, new NormalSpeedStrategy(), 1000000);
        }
    }
}
