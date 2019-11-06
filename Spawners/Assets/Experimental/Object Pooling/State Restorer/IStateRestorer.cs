namespace Experimental.ObjectPooling.StateRestorer
{
    public interface IStateRestorer<in T>
    {
        void OnRetrieve(T pooled);
        void OnReturn(T returned);
    }
}