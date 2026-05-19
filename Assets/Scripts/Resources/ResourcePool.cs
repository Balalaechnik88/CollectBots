using System.Collections.Generic;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Resource _prefab;
    [SerializeField] private int _poolCapacity = 10;

    private Queue<Resource> _pool = new Queue<Resource>();

    private void Awake()
    {
        for (int i = 0; i < _poolCapacity; i++)
        {
            Resource resource = Instantiate(_prefab, transform);
            resource.gameObject.SetActive(false);
            _pool.Enqueue(resource);
        }
    }

    public Resource Get()
    {
        Resource resource;

        if (_pool.Count == 0)
        {
            resource = Instantiate(_prefab);
        }
        else
        {
            resource = _pool.Dequeue();
        }

        resource.transform.SetParent(null, false);
        resource.gameObject.SetActive(true);

        return resource;
    }

    public void Release(Resource resource)
    {
        resource.gameObject.SetActive(false);
        resource.transform.SetParent(transform,false);
        _pool.Enqueue(resource);
    }
}