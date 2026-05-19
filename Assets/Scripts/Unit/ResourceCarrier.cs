using UnityEngine;

public class ResourceCarrier : MonoBehaviour
{
    [SerializeField] private Transform _carryPoint;

    private Resource _carriedResource;

    public void Grab(Resource resource)
    {
        _carriedResource = resource;
        _carriedResource.transform.SetParent(_carryPoint);
        _carriedResource.transform.localPosition = Vector3.zero;
    }

    public Resource Drop()
    {
        Resource droppedResource = _carriedResource;
        droppedResource.transform.SetParent(null);
        _carriedResource = null;

        return droppedResource;
    }
}