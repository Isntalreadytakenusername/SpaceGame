using Merlin2d.Game.Actors;

namespace GameCore.Commands
{
    public class Fall<T> : IAction<T> where T : IActor
    {
        private int step;
        private int dx;
        private int dy;

        public Fall()
        {
            this.step = 2;
            this.dx = 0;
            this.dy = 1;
        }

        public void Execute(T value)
        {
            int oldX = value.GetX();
            int oldY = value.GetY();
            int newX = value.GetX() + step * dx;
            int newY = value.GetY() + step * dy;

            value.SetPosition(newX, newY);

            if (value.GetWorld().IntersectWithWall(value))
                value.SetPosition(oldX, oldY);
        }
    }
}
