using System;

public interface IPoolable<T>
{
    event Action<T> OnReturnToPool;

    void ReturnToPool();
}