using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMotion : MonoBehaviour
{
    [SerializeField]
    int speed = 0;

    Transform myTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
