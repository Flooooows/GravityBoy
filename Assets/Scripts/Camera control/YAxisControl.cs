using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAxisControl : MonoBehaviour
{

    [SerializeField]
    Transform cible;

    Transform monTransform;

    Vector3 positionOriginel;

    // Start is called before the first frame update
    void Start()
    {
        monTransform = transform;
        positionOriginel = monTransform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (cible != null)
            monTransform.position = new Vector3(positionOriginel.x, cible.position.y, positionOriginel.z);
    }
}
