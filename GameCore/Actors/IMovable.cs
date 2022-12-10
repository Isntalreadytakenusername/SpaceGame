namespace GameCore.Actors
{
    public interface IMovable
    {
        public int GetX();
        public int GetY();
        void SetSpeedStrategy(ISpeedStrategy strategy);
        
        double GetSpeed();
    }
}
