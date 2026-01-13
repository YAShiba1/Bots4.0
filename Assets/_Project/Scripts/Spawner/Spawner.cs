using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IPoolable<T>
{
    [SerializeField] protected T Prefab;
    [SerializeField] private Transform _container;
    [SerializeField] private int _capacity = 20;

    private Stack<T> _pool;

    private void Awake()
    {
        _pool = new Stack<T>();

        for (int i = 0; i < _capacity; i++)
        {
            T item = Create();

            _pool.Push(item);
        }
    }

    public T Spawn(Vector3 position, Quaternion rotation)
    {
        T item = _pool.Count > 0 ? _pool.Pop() : Create();

        item.gameObject.SetActive(true);
        item.transform.SetPositionAndRotation(position, rotation);
        return item;
    }

    public T Spawn(Transform target)
    {
        return Spawn(target.position, target.rotation);
    }

    public void Release(T item)
    {
        if (item == null) return;

        item.gameObject.SetActive(false);
        item.transform.SetParent(_container);

        _pool.Push(item);
    }

    private T Create()
    {
        T item = Instantiate(Prefab, _container);

        item.gameObject.SetActive(false);

        item.OnReturnToPool += Release;

        return item;
    }
}
