using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnLevelComplete : MonoBehaviour
{

    [Header("Events")]
    public UnityEvent onLevelComplete;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onLevelComplete.Invoke();
    }
}
