using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pooling
{
    static System.Func<GameObject, GameObject> fallbackAcquireDelegate = DefaultAcquireFallback;
    static System.Func<GameObject, bool> fallbackReleaseDelegate = DefaultReleaseFallback;

    public static GameObject Acquire(GameObject obj, System.Func<GameObject, GameObject> fallback)
    {
        var poolable = obj.GetComponent<Poolable>();
        if (ReferenceEquals(poolable, null))
            return fallback(obj);
        return poolable.Acquire().gameObject;
    }

    public static GameObject Acquire(GameObject obj)
    {
        return Acquire(obj, fallbackAcquireDelegate);
    }

    public static bool Release(GameObject obj, System.Func<GameObject, bool> fallback)
    {
        var poolable = obj.GetComponent<Poolable>();
        if (ReferenceEquals(poolable, null))
            return fallback(obj);
        return poolable.Release();
    }

    public static bool Release(GameObject obj)
    {
        return Release(obj, fallbackReleaseDelegate);
    }

    static GameObject DefaultAcquireFallback(GameObject obj)
    {
        return Object.Instantiate(obj);
    }
    static bool DefaultReleaseFallback(GameObject obj)
    {
        try
        {
            DeathManager deathManager = obj.GetComponent<DeathManager>();
            if(deathManager != null)
            {
                deathManager.simpleDeath();
            }
            Object.Destroy(obj);
            return true;
        }
        catch
        {
            return false;
        }
    }

}
