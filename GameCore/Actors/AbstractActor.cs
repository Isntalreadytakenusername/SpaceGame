using Merlin2d.Game;
using Merlin2d.Game.Actors;

namespace GameCore.Actors
{
    public abstract class AbstractActor : IActor
    {
        private int posX;
        private int posY;
        private Animation animation;
        private IWorld world;
        private string name;
        private bool isRemoved = false;
        private bool isGravityEnabled = false;

        public AbstractActor()
        {
            name = "";
        }

        public AbstractActor(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return this.name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public int GetX()
        {
            return posX;
        }

        public int GetY()
        {
            return posY;
        }

        public int GetHeight()
        {
            return this.animation.GetHeight();
        }


        public int GetWidth()
        {
            return this.animation.GetWidth();
        }

        public void SetPosition(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
        }

        public void OnAddedToWorld(IWorld world)
        {
            this.world = world;
        }

        public IWorld GetWorld()
        {
            return this.world;
        }

        public Animation GetAnimation()
        {
            return this.animation;
        }

        public void SetAnimation(Animation animation)
        {
            this.animation = animation;
        }

        public bool IntersectsWithActor(IActor actor)
        {
            if ((this.posX < actor.GetX() - GetWidth()) || (this.posX > actor.GetX() + actor.GetWidth()))
            {
                return false;
            }

            if ((this.posY < actor.GetY() - GetHeight()) || (this.posY > actor.GetY() + actor.GetHeight()))
            {
                return false;
            }
            return true;
        }

        public void SetPhysics(bool isPhysicsEnabled)
        {
            isGravityEnabled = isPhysicsEnabled;
        }

        public bool IsAffectedByPhysics()
        {
            return isGravityEnabled;
        }

        public bool RemovedFromWorld()
        {
            return isRemoved;
        }

        public void RemoveFromWorld()
        {
            isRemoved = true;
        }

        public abstract void Update();
    }
}
