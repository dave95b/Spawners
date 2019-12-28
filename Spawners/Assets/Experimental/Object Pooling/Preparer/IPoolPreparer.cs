namespace Experimental.ObjectPooling.Preparer
{
    public interface IPoolPreparer<T>
    {
        IPool<T> Pool { get; }
    }
}