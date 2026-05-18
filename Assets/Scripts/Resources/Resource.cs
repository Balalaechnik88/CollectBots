using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsTargeted { get; private set; }

    public void SetTargeted()
    {
        IsTargeted = true;
    }
}