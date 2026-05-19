using System.Collections;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private ResourcePool _pool;
    [SerializeField] private float _spawnRadius = 15f;
    [SerializeField] private float _spawnDelay = 3f;
    [SerializeField] private float _spawnHeight = 0.5f;

    private WaitForSeconds _waitDelay;

    private void Awake()
    {
        _waitDelay = new WaitForSeconds(_spawnDelay);
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (enabled)
        {
            yield return _waitDelay;
            Spawn();
        }
    }

    private void Spawn()
    {
        Vector2 randomCircle = Random.insideUnitCircle * _spawnRadius;
        Vector3 spawnPosition = new Vector3(transform.position.x + randomCircle.x, _spawnHeight, transform.position.z + randomCircle.y);

        Resource newResource = _pool.Get();
        newResource.transform.position = spawnPosition;
    }
}