using System;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private BotMovement _movement;
    [SerializeField] private Transform _grabPoint;

    private Vector3 _startPosition;
    private Vector3 _basePosition;

    private ICollectable _currentTarget;

    public event Action<Bot> Freed;

    public bool HasTarget => _currentTarget != null;

    public bool IsCarrying { get; private set; } = false;

    private void Update()
    {
        if (IsCarrying)
        {
            _movement.MoveTo(_basePosition);
        }
        else if (_currentTarget != null)
        {
            _movement.MoveTo(_currentTarget.Position);
        }
        else if (transform.position.IsCloseToXZ(_startPosition) == false)
        {
            _movement.MoveTo(_startPosition);
        }
        else
        {
            _movement.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectable resource))
        {
            if (resource == _currentTarget)
            {
                resource.PickUp(_grabPoint);
                IsCarrying = true;
            }
        }

        if (other.TryGetComponent(out Base goldBase))
        {
            if (_currentTarget != null)
            {
                _currentTarget.Deliver();
                _currentTarget = null;
                IsCarrying = false;

                goldBase.CollectGold();

                Freed?.Invoke(this);
            }
        }
    }

    public void Initialize(Vector3 startPosition, Vector3 basePosition)
    {
        _startPosition = startPosition;
        _basePosition = basePosition;
    }

    public void SetTarget(ICollectable resource)
    {
        _currentTarget = resource;
    }
}
