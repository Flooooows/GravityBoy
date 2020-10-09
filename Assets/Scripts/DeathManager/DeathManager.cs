using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathManager : MonoBehaviour
{
   
        [Header("Events")]
    public UnityEvent onSimpleDeath;

    public void simpleDeath()
    {
        onSimpleDeath.Invoke();
    }
    
}
