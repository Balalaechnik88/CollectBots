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

    public event Action<Unit> OnBecameIdle;
    public event Action OnResourceDelivered;

    public bool IsIdle { get; private set; } = true;

    private void Update()
    {
        if (IsIdle)
        {
            return;
        }

        if (_isCarrying == false)
        {
            if (_targetResource == null)
            {
                ResetUnit();
                return;
            }

            if (_mover.HasReachedDestination(_interactDistance))
            {
                _carrier.PickUp(_targetResource);
                _isCarrying = true;
                _mover.MoveTo(_baseTransform.position);
            }
        }
        else
        {
            if (_mover.HasReachedDestination(_interactDistance))
            {
                _carrier.DropAndDestroy();
                OnResourceDelivered?.Invoke();
                ResetUnit();
            }
        }
    }
    public void AssignTask(Resource resource, Transform baseTransform)
    {
        _targetResource = resource;
        _baseTransform = baseTransform;
        _targetResource.SetTargeted();

        IsIdle = false;
        _isCarrying = false;
        _mover.MoveTo(_targetResource.transform.position);
    }

    private void ResetUnit()
    {
        _targetResource = null;
        _isCarrying = false;
        IsIdle = true;
        OnBecameIdle?.Invoke(this);
    }
}