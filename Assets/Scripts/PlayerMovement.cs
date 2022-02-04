using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Utilities
    private Rigidbody2D _body;
    private Animator _animator;
    private BoxCollider2D _collider;
    
    // User Input
    private float _horizontalInput;
    private bool _verticalInput;
    
    // Serialized Fields
    [Range(0, 100)] public float speed;
    [Range(0, 100)] public float jumpHeight;
    [SerializeField] public LayerMask groundLayer;
    
    // Animations
    private static readonly int AnimRun = Animator.StringToHash("run");
    private static readonly int AnimGrounded = Animator.StringToHash("grounded");
    private static readonly int AnimJump = Animator.StringToHash("jump");


    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }
    
    private void Update()
    {
     
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetButtonDown("Jump");
        
        _body.velocity = new Vector2(_horizontalInput * speed, _body.velocity.y);
        
        var playerTransform = transform;
        var localScale = playerTransform.localScale;
        
        // Flip player if facing left
        if (_horizontalInput > .01f)
        {
            localScale = new Vector3(Math.Abs(localScale.x), localScale.y, localScale.z);
        }
        else if(_horizontalInput < -.01f)
        {
            localScale = new Vector3(-Math.Abs(localScale.x), localScale.y, localScale.z);
        }
        
        playerTransform.localScale = localScale;

        
        if (_verticalInput && IsGrounded())
        {
            Jump();
        }
        
        UpdateAnimations();
    }

    private void Jump()
    {
        _body.velocity = new Vector2(_body.velocity.x, jumpHeight);
        UpdateAnimations();
        _animator.SetTrigger(AnimJump);
    }

    private void UpdateAnimations()
    {
        _animator.SetBool(AnimGrounded, IsGrounded());
        _animator.SetBool(AnimRun, _horizontalInput != 0);
    }

    private bool IsGrounded()
    {
        var raycastHit2D = Physics2D.BoxCast(
            _collider.bounds.center,
            _collider.size,
            0,
            Vector2.down,
            0.1f,
            groundLayer);
        return raycastHit2D.collider != null;
    }
}
