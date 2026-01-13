using System;
using UnityEngine;

public abstract class CollectableResource<T> : MonoBehaviour, ICollectable, IPoolable<T> where T : MonoBehaviour
{
    [SerializeField] private Collider _collider;

    public abstract event Action<T> OnReturnToPool;

    public bool IsBusy { get; private set; }

    public Vector3 Position => transform.position;

    public abstract void ReturnToPool();

    public void Reserve() => IsBusy = true;

    public void PickUp(Transform grabPoint)
    {
        IsBusy = true;

        transform.SetParent(grabPoint);
        transform.SetPositionAndRotation(grabPoint.position, Quaternion.identity);

        _collider.enabled = false;
    }

    public void Deliver()
    {
        ReturnToPool();
    }

    protected void ResetState()
    {
        IsBusy = false;

        transform.SetParent(null);
        transform.rotation = Quaternion.identity;

        _collider.enabled = true;
    }
}
