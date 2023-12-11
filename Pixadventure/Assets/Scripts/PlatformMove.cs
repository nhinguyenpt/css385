using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private Vector2 moveDistance;
    [SerializeField] private float moveSpeed;
    

    private bool _moveRight;
    private float _leftEdge;
    private float _rightEdge;
    
    private bool _moveUp;
    private float _upperLimit;
    private float _lowerLimit;

    private void Awake()
    {
        _leftEdge = transform.position.x;
        _rightEdge = transform.position.x + moveDistance.x;
        _moveRight = true;

        _moveUp = true;
        _lowerLimit = transform.position.y;
        _upperLimit = transform.position.y + moveDistance.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveDistance.x > 0) MoveHorizontal();
        if (moveDistance.y > 0) MoveVertical();
    }

    private void MoveHorizontal()
    {
        if (_moveRight)
        {
            if (transform.position.x > _rightEdge)
            {
                _moveRight = false;
            }
            else
            {
                transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        } else 
        {
            if (transform.position.x < _leftEdge)
            {
                _moveRight = true;
            }
            else
            {
                transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }
    }
    
    private void MoveVertical()
    {
        if (_moveUp)
        {
            if (transform.position.y > _upperLimit)
            {
                _moveUp = false;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime, transform.position.z);
            }
        } else 
        {
            if (transform.position.y < _lowerLimit)
            {
                _moveUp = true;
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
