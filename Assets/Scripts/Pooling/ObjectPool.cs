using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    List<Poolable> pool = new List<Poolable>();
    Poolable prefab;

    public ObjectPool(Poolable prefab)
    {
        this.prefab = prefab;
    }

    public Poolable Aquire()
    {
        Poolable obj;
        if (pool.Count > 0)
        {
            obj = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            obj.IsPooled = false;
            return obj;
        }
        obj = Object.Instantiate(prefab);
        obj.IsPooled = false;
        return obj;
    }

    public bool Release(Poolable obj)
    {
        if(!obj.IsPooled)
        {
            obj.IsPooled = true;
            pool.Add(obj);
            return true;
        }
        return false;
    }

    public void Remove(Poolable obj)
    {
        if (obj.IsPooled)
        {
            obj.IsPooled = false;
            pool.Remove(obj);
        }
    }
}
