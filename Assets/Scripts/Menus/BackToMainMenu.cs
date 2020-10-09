using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BackToMainMenu : MonoBehaviour
{
    public UnityEvent onKeydown;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) {
            onKeydown.Invoke();
        }
    }
}
