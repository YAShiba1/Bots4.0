using System;
using UnityEngine;

public class Gold : CollectableResource<Gold>
{
    public override event Action<Gold> OnReturnToPool;

    public override void ReturnToPool()
    {
        ResetState();

        OnReturnToPool?.Invoke(this);
    }
}
