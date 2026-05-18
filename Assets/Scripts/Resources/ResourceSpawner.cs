using System.Collections;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Resource _resourcePrefab;
    [SerializeField] private float _spawnRadius = 15f;
    [SerializeField] private float _spawnDelay = 3f;

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
        while (true)
        {
            yield return _waitDelay;
            Spawn(_resourcePrefab, transform.position, _spawnRadius);
        }
    }

    public void Spawn(Resource prefab, Vector3 center, float radius)
    {
        Vector2 randomCircle = Random.insideUnitCircle * radius;
        Vector3 spawnPosition = new Vector3(center.x + randomCircle.x, 0.5f, center.z + randomCircle.y);
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}