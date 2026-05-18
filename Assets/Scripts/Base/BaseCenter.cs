using System.Collections.Generic;
using UnityEngine;

public class BaseCenter : MonoBehaviour
{
    [SerializeField] private ResourceScanner _scanner;
    [SerializeField] private ResourceStorage _storage;
    [SerializeField] private List<Unit> _units;

    private Queue<Unit> _idleUnits = new Queue<Unit>();

    private void Start()
    {
        foreach (Unit unit in _units)
        {
            _idleUnits.Enqueue(unit);
            unit.OnBecameIdle += HandleUnitIdle;
            unit.OnResourceDelivered += HandleResourceDelivered;
        }
    }

    private void OnDestroy()
    {
        foreach (Unit unit in _units)
        {
            unit.OnBecameIdle -= HandleUnitIdle;
            unit.OnResourceDelivered -= HandleResourceDelivered;
        }
    }

    private void Update()
    {
        if (_idleUnits.Count == 0)
        {
            return;
        }

        if (_scanner.TryFindUnassignedResource(transform.position, out Resource resource))
        {
            Unit availableUnit = _idleUnits.Dequeue();
            availableUnit.AssignTask(resource, transform);
        }
    }

    private void HandleUnitIdle(Unit unit)
    {
        _idleUnits.Enqueue(unit);
    }

    private void HandleResourceDelivered()
    {
        _storage.AddResource();
    }
}