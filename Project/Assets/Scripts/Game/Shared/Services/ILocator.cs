namespace Game.Shared.Services
{
    public interface ILocator
    {
        public T Get<T>();
    }
}