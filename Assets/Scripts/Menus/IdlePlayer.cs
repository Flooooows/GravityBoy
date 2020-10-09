using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlayer : MonoBehaviour
{
    [SerializeField]
    Animator animator = null;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
        {
            Debug.Log(animator);
            return;
        }
        animator.SetFloat("HorizontalSpeed", 0);
        animator.SetFloat("VerticalSpeed", 0);
        animator.SetBool("IsGrounded", true);
        animator.SetBool("IsDying", false);
    }
}

