using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    int damageValue = 0;
    [SerializeField]
    LayerMask canDamage = 0;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;
        DealDamage(rb);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var rb = collider.attachedRigidbody;
        if(!ReferenceEquals(rb, null))
            DealDamage(rb);
    }

    private void DealDamage(Rigidbody2D rb)
    {
        if (canDamage == (canDamage | 1 << rb.gameObject.layer))
        {
            var damageable = rb.GetComponent<IDamageable>();
            if (!ReferenceEquals(damageable, null))
                damageable.TakeDamage(damageValue);
        }
    }
}
