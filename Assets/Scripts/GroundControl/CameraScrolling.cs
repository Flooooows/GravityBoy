using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScrolling : MonoBehaviour
{

    [SerializeField]
    float cameraSpeed;

    private bool actived = true;

    Rigidbody2D myrigidbody2D;

    public bool Actived { get => actived; set => actived = value; }

    // Start is called before the first frame update
    void Start()
    {
        myrigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveGround();
    }

    private void MoveGround()
    {
        if (actived)
        {
            myrigidbody2D.velocity = new Vector2((-cameraSpeed), myrigidbody2D.velocity.y);
        }else
        {
            myrigidbody2D.velocity = new Vector2(0, 0);
        }
    }
}
