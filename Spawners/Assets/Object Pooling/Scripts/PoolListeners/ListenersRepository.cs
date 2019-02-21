using UnityEngine;
using System.Collections.Generic;

namespace ObjectPooling
{
    public abstract class ListenersRepository<T> : MonoBehaviour
    {
        private PoolListener<T>[] listeners;
        public PoolListener<T>[] Listeners
        {
            get
            {
                if (listeners is null)
                    listeners = GetListeners();
                return listeners;
            }
        }

        protected abstract PoolListenerProvider<T>[] ListenerProviders { get; }

        private PoolListener<T>[] GetListeners()
        {
            int length = ListenerProviders.Length;
            var listeners = new PoolListener<T>[length];

            for (int i = 0; i < length; i++)
                listeners[i] = ListenerProviders[i].Listener;

            return listeners;
        }
    }
}