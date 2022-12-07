namespace GameCore.Commands
{
    public interface IAction<T>
    {
        public void Execute(T value);
    }
}
