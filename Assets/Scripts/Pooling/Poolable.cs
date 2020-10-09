using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    public ObjectPool pool;

    bool isPooled;
    bool isBeingDestroyed;

    public bool IsPooled
    {
        get
        {
            return isPooled;
        }
        set
        {
            isPooled = value;
            if (!isBeingDestroyed)
                gameObject.SetActive(!value);
        }
    }

    public Poolable Acquire()
    {
        if (ReferenceEquals(pool, null))
            pool = new ObjectPool(this);
        var obj = pool.Aquire();
        if (ReferenceEquals(obj.pool, null))
            obj.pool = this.pool;
        return obj;
    }

    public bool Release()
    {
        if (ReferenceEquals(pool, null))
            return false;
        return pool.Release(this);
    }

    public void ReleaseImmediate()
    {
        if (!ReferenceEquals(pool, null))
            pool.Release(this);
        else
            Destroy(gameObject);
    }

    public void RemoveFromPool()
    {
        if (pool != null)
        {
            pool.Remove(this);
            pool = null;
        }
    }

    private void OnDestroy()
    {
        isBeingDestroyed = true;
        RemoveFromPool();
    }
}

