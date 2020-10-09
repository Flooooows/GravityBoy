using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simplerecycler : MonoBehaviour
{
    [SerializeField]
    float decalage;

    [SerializeField]
    GameObject lastPlaced;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObj = collision.attachedRigidbody.gameObject;
        collisionObj.transform.position = new Vector3(lastPlaced.transform.position.x + decalage, collisionObj.transform.position.y, collisionObj.transform.position.z);
        lastPlaced = collisionObj;
    }
}
