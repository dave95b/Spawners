namespace Experimental.ObjectPooling.StateRestorer
{
    public class EmptyStateRestorer<T> : IStateRestorer<T>
    {
        private static EmptyStateRestorer<T> instance;
        public static EmptyStateRestorer<T> Instance => instance ?? (instance = new EmptyStateRestorer<T>());

        private EmptyStateRestorer() { }

        public void OnRetrieve(T pooled) { }

        public void OnReturn(T returned) { }
    }
}