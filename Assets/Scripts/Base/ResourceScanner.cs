using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius = 50f;
    [SerializeField] private LayerMask _resourceLayer;

    public bool TryFindUnassignedResource(Vector3 center, out Resource foundResource)
    {
        foundResource = null;
        Collider[] hits = Physics.OverlapSphere(center, _scanRadius, _resourceLayer);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Resource resource))
            {
                if (resource.IsTargeted == false)
                {
                    foundResource = resource;
                    return true;
                }
            }
        }

        return false;
    }
}