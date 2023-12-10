using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float scale;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravity;
    [SerializeField] private LayerMask groundLayer;

    [Header("Jump Clip")]
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private EdgeCollider2D _bodyCollider;
    private BoxCollider2D _feetCollider2D;
    private float _horizontalInput;
    private bool _isOnPlatform;

    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _bodyCollider = GetComponent<EdgeCollider2D>();
        _feetCollider2D = GetComponent<BoxCollider2D>();
        _isOnPlatform = false;
    }

    private void AdjustScale()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        //Flip player when moving left-right
        if (_horizontalInput > 0.01f)
            transform.localScale = new Vector3(scale, scale);
        else if (_horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1.0f * scale, scale);
    }

    private void Update()
    {
        AdjustScale();
        
        //Set animator parameters
        _animator.SetBool("run", _horizontalInput != 0);
        _animator.SetBool("grounded", isGrounded());

        if (Input.GetKey(KeyCode.Space))
            Jump();
    }

    private void FixedUpdate()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        float horizontalVelocity = _horizontalInput * speed;
        
        _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
        _rigidbody.gravityScale = gravity;
    }

    private void Jump()
    {
        if (isGrounded() || _isOnPlatform)
        {
            SoundManager.instance.PlaySound(jumpSound);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpPower);
            _animator.SetTrigger("jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("MovingPlatform"))
        {
            print("Enter platform");
            _isOnPlatform = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("MovingPlatform"))
        {
            print("Exit platform");
            _isOnPlatform = false;
        }

        Debug.Log(collision.gameObject);
    }

    private bool isGrounded()
    {
        // RaycastHit2D raycastHit = Physics2D.BoxCast(_bodyCollider.bounds.center, _bodyCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        RaycastHit2D raycastHit = Physics2D.BoxCast(_feetCollider2D.bounds.center, 
            _feetCollider2D.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null || _isOnPlatform;
    }
}