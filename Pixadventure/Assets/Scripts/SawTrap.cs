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
        // Horizontal Movement
        if (right && transform.position.x > rightEdge)
        {
            right = false;
        }
        else if (!right && transform.position.x < leftEdge)
        {
            right = true;
        }
        else
        {
            float horizontalChange = (right ? 1 : -1) * moveSpeed * Time.deltaTime;
            transform.position += new Vector3(horizontalChange, 0, 0);
        }

        // Vertical Movement
        if (up && transform.position.y > upperLimit)
        {
            up = false;
        }
        else if (!up && transform.position.y < lowerLimit)
        {
            up = true;
        }
        else
        {
            float verticalChange = (up ? 1 : -1) * moveSpeed * Time.deltaTime;
            transform.position += new Vector3(0, verticalChange, 0);
        }
    }
}
