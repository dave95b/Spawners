namespace ObjectManagement.ObjectPooling.Factory
{
    public class PooledObjectFactory<T> : IPooledFactory<T> where T : new()
    {
        public T Create()
        {
            return new T();
        }
    }
}