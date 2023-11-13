using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private Vector2 moveDistance;
    [SerializeField] private float moveSpeed;

    private bool right;
    private float rightEdge;
    private float leftEdge;
    private bool up;
    private float upperLimit;
    private float lowerLimit;

    private void Awake()
    {
        leftEdge = transform.position.x;
        rightEdge = transform.position.x + moveDistance.x;
        lowerLimit = transform.position.y;
        upperLimit = transform.position.y + moveDistance.y;
        right = true;
        up = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }

    private void Update()
    {
        if (right)
        {
            if (transform.position.x > rightEdge)
            {
                right = false;
            }
            else
            {
                transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }  
        else
        {
            if (transform.position.x < leftEdge)
            {
                right = true;
            }
            else
            {
                transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }

        if (up)
        {
            if (transform.position.y > upperLimit)
            {
                print("SawTrap: change to down");
                up = false;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime, transform.position.z);
            }
        }
        else
        {
            if (transform.position.y < lowerLimit)
            {
                print("SawTrap: change to up");
                up = true;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime, transform.position.z);
            }
        }

    }
}
