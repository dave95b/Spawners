using UnityEngine;
using System.Collections.Generic;

namespace Experimental.ObjectPooling.StateRestorer
{
    public class EmptyStateRestorer<T> : IStateRestorer<T>
    {
        private static EmptyStateRestorer<T> instance;
        public static EmptyStateRestorer<T> Instance
        {
            get
            {
                if (instance is null)
                    instance = new EmptyStateRestorer<T>();

                return instance;
            }
        }

        private EmptyStateRestorer() {}

        public void OnRetrieve(T pooled) {}

        public void OnReturn(T returned) {}
    }
}