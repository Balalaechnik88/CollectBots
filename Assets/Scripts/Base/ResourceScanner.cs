using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private float _radius = 50f;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private ResourceTracker _tracker;

    private void Update()
    {
        Scan();
    }

    private void Scan()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius, _layer);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Resource resource))
            {
                _tracker.RegisterFoundResource(resource);
            }
        }
    }
}