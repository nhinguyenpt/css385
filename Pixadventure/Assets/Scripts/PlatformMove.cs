using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveSpeed;    
    [SerializeField] private bool moveHorizontal;    

    private bool moveRight;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.position.x;
        rightEdge = transform.position.x + moveDistance;
        moveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveRight)
        {
            if (transform.position.x > rightEdge)
            {
                moveRight = false;
            }
            else
            {
                transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        } else 
        {
            if (transform.position.x < leftEdge)
            {
                moveRight = true;
            }
            else
            {
                transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {        
        if (collision.gameObject.tag == "Player")
        {
            if (moveRight)
            {
                movement.SetExternalVelocity(moveSpeed);
            } else
            {
                movement.SetExternalVelocity(-1 * moveSpeed);
            }
        }
    }
}
