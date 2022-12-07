using GameCore.Actors;
using Merlin2d.Game;
using Merlin2d.Game.Actions;
using Merlin2d.Game.Actors;

namespace GameCore.Commands
{
    public class Move : ICommand
    {
        private IActor actor;
        private double step;
        private double dx;
        private double dy;
        private double accumulatedX;
        private double accumulatedY;

        public Move(IActor movable, double step, int dx, int dy)
        {
            if (!(movable is IActor))
            {
                throw new ArgumentException("Can only move actors");
            }

            this.actor = (IActor)movable;
            this.step = step;
            this.dx = dx;
            this.dy = dy;
        }

        // simple math functions for readability of code
        private double GetDecimalPart(double value)
        {
            {
                return value - Math.Floor(value);
            }
        }

        private int GetIntegralPart(double value)
        {
            return (int)Math.Floor(value);
        }

        public bool BeyondMapBoundaries()
        {
            {
                if (actor.GetX() + dx < 0 || actor.GetX() + dx > 950 - actor.GetWidth()) // good old hardcoding, as world object doesnt have getting width and height apparently
                {
                    return true;
                }
                if (actor.GetY() + dy < 0 || actor.GetY() + dy > 880 - actor.GetHeight())
                {
                    return true;
                }
                return false;
            }
        }

        public void Execute()
        {
            int oldX = this.actor.GetX();
            int oldY = this.actor.GetY();
            int newX = oldX;
            int newY = oldY;
            
            // 1. Add whole steps first to new coordinates
            newX += GetIntegralPart(step * dx);
            newY += GetIntegralPart(step * dy);

            // 2. Add accumulated leftover part-steps to accumulation variables
            accumulatedX += GetDecimalPart(step * dx);
            accumulatedY += GetDecimalPart(step * dy);

            // 3. Get the whole stuff out of accumulated values and add it to new coordinates
            newX += GetIntegralPart(accumulatedX);
            newY += GetIntegralPart(accumulatedY);
            
            // 4. Update accumulated values. It makes sense to add only decimal part now
            accumulatedX = GetDecimalPart(accumulatedX);
            accumulatedY = GetDecimalPart(accumulatedY);

            this.actor.SetPosition(newX, newY);

            try
            {
                if (this.actor.GetWorld().IntersectWithWall(this.actor) || BeyondMapBoundaries())
                    this.actor.SetPosition(oldX, oldY);
            }
            catch (Exception e)
            {
                this.actor.GetWorld().RemoveActor(this.actor); // yes, smart exception handling
            }
        }
    }
}
