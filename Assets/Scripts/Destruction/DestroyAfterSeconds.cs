using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField]
    float lifeTime = 0f;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(DestroyCoroutine());
    }
    void OnDisable()
    {
        StopAllCoroutines();
    }


    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Pooling.Release(gameObject);
    }
}
