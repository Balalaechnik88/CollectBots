using System.Collections.Generic;
using UnityEngine;

public class ResourceTracker : MonoBehaviour
{
    private List<Resource> _availableResources = new List<Resource>();
    private List<Resource> _allocatedResources = new List<Resource>();

    public void RegisterFoundResource(Resource resource)
    {
        if (_availableResources.Contains(resource) == false && !_allocatedResources.Contains(resource) == false)
        {
            _availableResources.Add(resource);
        }
    }

    public bool TryAllocateResource(out Resource resource)
    {
        resource = null;

        if (_availableResources.Count > 0)
        {
            resource = _availableResources[0];
            _availableResources.RemoveAt(0);
            _allocatedResources.Add(resource);
            return true;
        }

        return false;
    }

    public void RemoveDeliveredResource(Resource resource)
    {
        _allocatedResources.Remove(resource);
    }
}