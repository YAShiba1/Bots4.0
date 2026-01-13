using UnityEngine;

public class Factory<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _container;

    public T Create(Transform spawnPoint)
    {
        T item = Instantiate(_prefab, spawnPoint.position, spawnPoint.rotation, _container);

        return item;
    }
}
