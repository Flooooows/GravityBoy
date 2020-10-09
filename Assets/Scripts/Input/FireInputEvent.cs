using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireInputEvent : MonoBehaviour
{
    [SerializeField]
    float fireInterval = 0f;

    public UnityEvent onFire;

    bool isShooting = false;

    private void Update()
    {
        bool shotRequested = Input.GetAxisRaw("Fire1") > 0.01f;
        if (shotRequested )
        {
            if (!isShooting)
            {
                isShooting = true;
                StartCoroutine(FireCoroutine());
            }
        }
        else
        {
            isShooting = false;
            StopAllCoroutines();
        }
    }

    IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            onFire.Invoke();
            yield return new WaitForSeconds(fireInterval);
        }
    }


}
