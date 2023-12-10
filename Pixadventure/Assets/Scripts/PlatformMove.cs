using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private Vector2 moveDistance;
    [SerializeField] private float moveSpeed;
    

    private bool moveRight;
    private float leftEdge;
    private float rightEdge;
    
    private bool moveUp;
    private float upperLimit;
    private float lowerLimit;

    private void Awake()
    {
        leftEdge = transform.position.x;
        rightEdge = transform.position.x + moveDistance.x;
        moveRight = true;

        moveUp = true;
        lowerLimit = transform.position.y;
        upperLimit = transform.position.y + moveDistance.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveDistance.x > 0) MoveHorizontal();
        if (moveDistance.y > 0) MoveVertical();
    }

    private void MoveHorizontal()
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
    
    private void MoveVertical()
    {
        if (moveUp)
        {
            if (transform.position.y > upperLimit)
            {
                moveUp = false;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime, transform.position.z);
            }
        } else 
        {
            if (transform.position.y < lowerLimit)
            {
                moveUp = true;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime, transform.position.z);
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
