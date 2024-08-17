using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private PlatformEffector2D _effector;

    private float _horizontalInput;
    private float _verticalInput;

    private string _horizontalAxis = "Horizontal";
    private string _verticalAxis = "Vertical";

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Player _player;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis(_horizontalAxis);
        _verticalInput = Input.GetAxis(_verticalAxis);

        //if (!_player.IsOnStair)
        //    _effector.rotationalOffset = 0;

    }

    private void FixedUpdate()
    {
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

        if (Input.GetKeyDown(KeyCode.Space) && _player.IsOnGround)
        {
            Jump();
        }
    }

    private void Move()
    {
        if (_horizontalInput != 0)
            _spriteRenderer.flipX = _horizontalInput > 0 ? false : true;

        _rb.velocity = new Vector2(_horizontalInput * _speed, _rb.velocity.y);
    }

    private void StairsMove()
    {
        if (_horizontalInput != 0)
            _spriteRenderer.flipX = _horizontalInput > 0 ? false : true;

        //_effector.rotationalOffset = (_verticalInput > 0) ? 0 : 180;

        _rb.velocity = new Vector2(_horizontalInput * _speed, _verticalInput * _speed);
    }

    private void Jump()
    {
        _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
    }
}
