public interface IPoolService
{
    T Get<T>() where T : class;
    void Release<T>(T obj) where T : class;
}
