using UnityEngine;

public interface ICollectable
{
    Vector3 Position { get; }

    public void PickUp(Transform carrier);
    public void Deliver();
}
