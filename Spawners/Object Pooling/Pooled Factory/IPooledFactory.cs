﻿namespace ObjectManagement.ObjectPooling.Factory
{
    public interface IPooledFactory<out T>
    {
        T Create();
    }
}