using System;
using System.Collections;
using UnityEngine;

public class GoldSpawner : Spawner<Gold>
{
    [Header("Spawn Settings")]
    [SerializeField] private float _radius;
    [SerializeField] private float _delay;

    public float Radius => _radius;

    public event Action Spawned; 

    private void Start()
    {
        StartCoroutine(SpawnWithDelay());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private IEnumerator SpawnWithDelay()
    {
        var waitForSeconds = new WaitForSeconds(_delay);

        while (enabled)
        {
            Vector2 randomPoint2D = UnityEngine.Random.insideUnitCircle * _radius;

            Vector3 randomPoint = new(
                randomPoint2D.x + transform.position.x,
                Prefab.transform.position.y,
                randomPoint2D.y + transform.position.z);

            Gold gold = Spawn(randomPoint, Quaternion.identity);

            Spawned?.Invoke();

            yield return waitForSeconds;
        }
    }
}
