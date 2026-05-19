using System.Collections.Generic;
using UnityEngine;

public class BaseCenter : MonoBehaviour
{
    [SerializeField] private ResourceTracker _tracker;
    [SerializeField] private ResourceStorage _storage;
    [SerializeField] private ResourcePool _pool;
    [SerializeField] private List<Unit> _units;

    private Queue<Unit> _idleQueue = new Queue<Unit>();

    private void Awake()
    {
        foreach (Unit unit in _units)
        {
            _idleQueue.Enqueue(unit);
            unit.OnTaskCompleted += HandleUnitTaskCompleted;
        }
    }

    private void OnDestroy()
    {
        foreach (Unit unit in _units)
        {
            unit.OnTaskCompleted -= HandleUnitTaskCompleted;
        }
    }

    private void Update()
    {
        if (_idleQueue.Count == 0)
        {
            return;
        }

        if (_tracker.TryAllocateResource(out Resource resource))
        {
            Unit unit = _idleQueue.Dequeue();
            unit.AssignTask(resource, transform);
        }
    }

    private void HandleUnitTaskCompleted(Unit unit, Resource deliveredResource)
    {
        _storage.RegisterDelivery();

        _tracker.RemoveDeliveredResource(deliveredResource);

        _pool.Release(deliveredResource);

        _idleQueue.Enqueue(unit);
    }
}