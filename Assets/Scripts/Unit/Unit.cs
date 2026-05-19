using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitMover _mover;
    [SerializeField] private ResourceCarrier _carrier;
    [SerializeField] private float _interactDistance = 1.5f;

    private Resource _targetResource;
    private Transform _baseTransform;
    private bool _isCarrying;

    public event Action<Unit, Resource> OnTaskCompleted;

    public bool IsIdle { get; private set; } = true;

    public void AssignTask(Resource resource, Transform baseTransform)
    {
        _targetResource = resource;
        _baseTransform = baseTransform;

        IsIdle = false;
        _isCarrying = false;
        _mover.MoveTo(_targetResource.transform.position);
    }

    private void Update()
    {
        if (IsIdle)
        {
            return;
        }

        if (_isCarrying == false)
        {
            HandleMovingToResource();
        }
        else
        {
            HandleReturningToBase();
        }
    }

    private void HandleMovingToResource()
    {
        if (_mover.HasReachedDestination(_interactDistance))
        {
            _carrier.Grab(_targetResource);
            _isCarrying = true;
            _mover.MoveTo(_baseTransform.position);
        }
    }

    private void HandleReturningToBase()
    {
        if (_mover.HasReachedDestination(_interactDistance))
        {
            Resource deliveredResource = _carrier.Drop();

            SetIdle();
            OnTaskCompleted?.Invoke(this, deliveredResource);
        }
    }

    private void SetIdle()
    {
        _targetResource = null;
        _isCarrying = false;
        IsIdle = true;
    }
}