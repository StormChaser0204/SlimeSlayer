namespace Game.Shared.Services
{
    public interface IProvider
    {
        public void Provide<T>(T serviceInstance);
    }
}