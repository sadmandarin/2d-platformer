using System;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private PlatformEffector2D _effector;
    [SerializeField] private GameObject _hpBar;
    [SerializeField] private Transform _wallCheck;

    private float _horizontalInput;
    private float _verticalInput;

    private string _horizontalAxis = "Horizontal";
    private string _verticalAxis = "Vertical";

    private float _stopForce = 50;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Player _player;
    private BoxCollider2D _boxCollider;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis(_horizontalAxis);
        _verticalInput = Input.GetAxis(_verticalAxis);

        if (_horizontalInput != 0 && Input.GetKeyDown(KeyCode.C))
        {
            if (!_player.IsRollingAnimationStart && _player.IsOnGround)
            {
                Roll();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && _player.IsOnGround)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        _player.SetFallingState(_rb.velocity.y);
        _player.SetMovingState(_horizontalInput == 0 ? 0 : 1);

        if (!_player.IsOnStair)
        {
            _rb.gravityScale = 1;
            Move();
        }
        else
        {
            _rb.gravityScale = 0;
            StairsMove();
        }
    }

    private void Roll()
    {
        _player.EnableRollingAnimationState();
    }

    private void Move()
    {
        Flip();

        if (_horizontalInput != 0 && !_player.IsTouchingWall)
        {
            _rb.velocity = new Vector2(_horizontalInput * _speed, _rb.velocity.y);
        }
        else if (_player.IsTouchingWall && Mathf.Sign(_horizontalInput) == Mathf.Sign(_rb.velocity.x))
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
    }

    private void StairsMove()
    {
        Flip();

        _rb.velocity = new Vector2(_horizontalInput * _speed, _verticalInput * _speed);
    }

    private void Jump()
    {
        _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);

        _player.SetJumpingState(true);
    }

    private void Flip()
    {
        if (_horizontalInput != 0)
        {
            if (_horizontalInput > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _hpBar.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else 
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                _hpBar.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        } 
    }
}
