using UnityEngine;

public class ResourceCarrier : MonoBehaviour
{
    [SerializeField] private Transform _carryPoint;

    public void PickUp(Resource resource)
    {
        resource.transform.SetParent(_carryPoint);
        resource.transform.localPosition = Vector3.zero;
    }

    public void DropAndDestroy()
    {
        if (_carryPoint.childCount > 0)
        {
            Transform resourceTransform = _carryPoint.GetChild(0);
            resourceTransform.SetParent(null);
            Destroy(resourceTransform.gameObject);
        }
    }
}