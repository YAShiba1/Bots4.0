using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private GoldSpawner _goldSpawner;
    [SerializeField] private LayerMask _goldLayerMask;
    [SerializeField] private float _delay;

    private HashSet<ICollectable> _resources = new();

    public event Action Scanned;

    private void Start()
    {
        StartCoroutine(ScanWithDelay());
    }

    public ICollectable GetResource()
    {
        ICollectable found = null;

        foreach (ICollectable resource in _resources)
        {
            if (resource != null && resource.IsBusy == false)
            {
                found = resource;
                resource.Reserve();
                break;
            }
        }

        if (found != null)
        {
            _resources.Remove(found);
        }

        return found;
    }

    private IEnumerator ScanWithDelay()
    {
        var waitForSeconds = new WaitForSeconds(_delay);

        while (enabled)
        {
            Scan(_goldSpawner.transform.position, _goldSpawner.Radius);
            Scanned?.Invoke();

            yield return waitForSeconds;
        }
    }

    private void Scan(Vector3 point, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(point, radius, _goldLayerMask);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out ICollectable resource))
            {
                if (resource.IsBusy == false)
                {
                    _resources.Add(resource);
                }
            }
        }
    }
}
