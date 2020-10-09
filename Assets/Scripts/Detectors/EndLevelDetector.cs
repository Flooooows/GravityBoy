using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndLevelDetector : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent onLevelend;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        onLevelend.Invoke();
    }

}
