﻿using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float scale;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravity;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;


    [Header("Jump Clip")]
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D body;
    private Animator anim;
    private EdgeCollider2D boxCollider;
    private float horizontalInput;
    private bool onPlatform;

    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<EdgeCollider2D>();
        onPlatform = false;
    }

    private void AdjustScale()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(scale, scale);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1.0f * scale, scale);
    }

    private void Update()
    {
        AdjustScale();
        
        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        if (Input.GetKey(KeyCode.Space))
            Jump();
    }

    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        float horizontalVelocity = horizontalInput * speed;
        
        body.velocity = new Vector2(horizontalVelocity, body.velocity.y);
        body.gravityScale = gravity;
    }

    private void Jump()
    {
        if (isGrounded() || onPlatform)
        {
            SoundManager.instance.PlaySound(jumpSound);
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("MovingPlatform"))
        {
            print("Enter platform");
            onPlatform = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("MovingPlatform"))
        {
            print("Exit platform");
            onPlatform = false;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null || onPlatform;
    }
}