namespace ObjectManagement.ObjectPooling.Preparer
{
    public interface IPoolPreparer<T>
    {
        IPool<T> Pool { get; }
    }
}