using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private List<ICollectable> _detectedResources = new();
    private List<ICollectable> _reservedResources = new();

    public event Action ResourceAvailable;

    public void RemoveResource(ICollectable resource)
    {
        _detectedResources.Remove(resource);
        _reservedResources.Remove(resource);
    }

    public ICollectable GetResource()
    {
        foreach (ICollectable resource in _detectedResources)
        {
            if (resource != null && _reservedResources.Contains(resource) == false)
            {
                _reservedResources.Add(resource);
                return resource;
            }
        }

        return null;
    }

    public void AddResource(ICollectable resource)
    {
        if (resource == null || _detectedResources.Contains(resource)) 
            return;

        _detectedResources.Add(resource);

        ResourceAvailable?.Invoke();
    }
}
