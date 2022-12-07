using Merlin2d.Game;
using Merlin2d.Game.Actors;

namespace GameCore.Commands
{
    public class Gravity : IPhysics
    {
        private IWorld world;
        private Fall<IActor> fall = new Fall<IActor>();

        public void Execute()
        {
            List<IActor> actors = this.world.GetActors();

            // Func<IActor, bool> func = x => x.IsAffectedByPhysics();
            // List<IActor> actors = this.world.GetActors().Where(func).ToList();

            foreach (IActor actor in actors)
            {
                if (actor.IsAffectedByPhysics())
                {
                    fall.Execute(actor);
                }
            }

            /*actors.ForEach(x =>
            {
                if (x.IsAffectedByPhysics())
                {
                    Move move = new Move(x, 2, 0, 1);
                    move.Execute();
                }
            });*/
        }

        public void SetWorld(IWorld world)
        {
            this.world = world;
        }
    }
}
