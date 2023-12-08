using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveSpeed;

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
    void FixedUpdate()
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
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("Player OnTriggerEnter2D " + this.name);
            collision.transform.SetParent(this.transform);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("Player OnTriggerExit2D " + this.name);
            collision.transform.SetParent(null);
        }
    }
}
