using UnityEngine;

public interface ICollectable
{
    public bool IsBusy { get; }

    Vector3 Position { get; }

    public void PickUp(Transform carrier);
    public void Reserve();
    public void Deliver();
}
