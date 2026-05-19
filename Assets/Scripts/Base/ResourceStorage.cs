using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    private int _count;

    public void RegisterDelivery()
    {
        _count++;
        Debug.Log($"Ресурсов на складе: {_count}");
    }
}