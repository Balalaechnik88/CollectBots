using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    private int _resourceCount = 0;

    public void AddResource()
    {
        _resourceCount++;
        Debug.Log($"Ресурсов на базе: {_resourceCount}");
    }
}