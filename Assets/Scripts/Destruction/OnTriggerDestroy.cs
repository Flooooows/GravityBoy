using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDestroy : MonoBehaviour
{
    [SerializeField]
    bool destroySelf = false;
    [SerializeField]
    bool destroyOther = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyOther)
            Pooling.Release(collision.attachedRigidbody.gameObject);
        if (destroySelf)
            Pooling.Release(gameObject);
    }
}
