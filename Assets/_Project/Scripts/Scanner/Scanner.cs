using System;
using System.Collections;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private int _maxScanResults = 50;
    [SerializeField] private LayerMask _goldLayerMask;

    private Collider[] _results;

    public event Action<ICollectable> ResourceDetected;

    private void Awake()
    {
        _results = new Collider[_maxScanResults];
    }

    private void Start()
    {
        StartCoroutine(ScanWithDelay());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private IEnumerator ScanWithDelay()
    {
        float delay = 0.5f;
        var waitForSeconds = new WaitForSeconds(delay);

        while (enabled)
        {
            Scan();

            yield return waitForSeconds;
        }
    }

    private void Scan()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _results, _goldLayerMask);

        for (int i = 0; i < count; i++)
        {
            if (_results[i].TryGetComponent(out ICollectable resource))
            {
                ResourceDetected?.Invoke(resource);
            }

            _results[i] = null;
        }
    }
}
