namespace SpawnerSystem.ObjectPooling
{
    public interface IPoolableStateRestorer<T>
    {
        void OnRetrieve(Poolable<T> poolable);
        void OnReturn(Poolable<T> poolable);
    }

    public class DefaultStateRestorer<T> : IPoolableStateRestorer<T>
    {
        public void OnRetrieve(Poolable<T> poolable)
        {
            poolable.gameObject.SetActive(true);
        }

        public void OnReturn(Poolable<T> poolable)
        {
            poolable.gameObject.SetActive(false);
        }
    }
}