using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CdProjeCt.EventTypes;

public class Health : MonoBehaviour, IDamageable
{
    [Header("Values")]
    [SerializeField]
    int initHealth = 0;
    [SerializeField]
    AudioClip onDamageSound;

    Transform myTransform;

    [Header("Events")]
    public UnityEvent onDeath;
    public TransformEvent onDeathTransform;

    int currenthealth;

    private void OnEnable()
    {
        myTransform = transform;
        currenthealth = initHealth;
    }

    public void TakeDamage(int damage)
    {
        myTransform.gameObject.GetComponent<AudioSource>().PlayOneShot(onDamageSound);
        currenthealth -= damage;
        if (currenthealth <= 0)
        {
            onDeathTransform.Invoke(myTransform);
            onDeath.Invoke();
        }
    }

    [ContextMenu("Test Damage")]
    public void TestDamage()
    {
        TakeDamage(1);
    }

}
