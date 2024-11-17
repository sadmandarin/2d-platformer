using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Класс движения игрока
/// </summary>
[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _wallCheck;

    private Rigidbody2D _rb;
    private Player _player;
    private Inputs _inputs;

    private bool _isJumping;
    private Vector2 _moveInput;
    private bool _jumpInput;
    private float swingForce = 5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponentInChildren<Player>();
        _inputs = new Inputs();
    }

    private void FixedUpdate()
    {
        if (!_player.IsStunned)
        {
            _player.SetFallingState(_rb.velocity.y);
            if (!_player.IsBlocking)
            {
                if (!_player.IsOnStair && !_player.IsOnRope)
                {
                    _rb.gravityScale = 1;
                    _player.SetMovingState(_moveInput.x == 0 ? 0 : 1);
                    Move();
                }
                else if(_player.IsOnStair)
                {
                    _rb.gravityScale = 0;
                    _player.SetMovingState(_moveInput.x == 0 ? 0 : 1);
                    StairsMove();
                }
                else if (_player.IsOnRope)
                {
                    RopeSwing();
                }
            }
            else
            {
                if (_player.IsOnGround)
                {
                    _rb.velocity = Vector2.zero;
                }
            }
        }
        else
        {
            if (_player.IsOnGround)
            {
                _rb.velocity = Vector2.zero;
            }
        }
    }

    private void OnJumpPerform(InputAction.CallbackContext context)
    {
        if (!_player.IsStunned && !_player.IsBlocking)
        {
            if ((_player.IsOnGround || _player.IsOnRope) && !_player.IsRolling)
            {
                if (!_player.IsJumping)
                {
                    Jump();
                }
            }
        }
    }

    private void OnRollPerform(InputAction.CallbackContext context)
    {
        if (!_player.IsStunned && !_player.IsBlocking)
        {
            if (_moveInput.x != 0)
            {
                if (!_player.IsRollingAnimationStart && _player.IsOnGround)
                {
                    Roll();
                }
            }
        }
    }

    public void Subscription()
    {
        //Actions
        _inputs.GamePlay.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();

        _inputs.GamePlay.Move.canceled += ctx => _moveInput = Vector2.zero;

        _inputs.GamePlay.Jump.performed += OnJumpPerform;

        _inputs.GamePlay.Roll.performed += OnRollPerform;

        _inputs.Enable();
    }

    public void Unsubscription()
    {
        _inputs.GamePlay.Move.performed -= ctx => _moveInput = ctx.ReadValue<Vector2>();

        _inputs.GamePlay.Move.canceled -= ctx => _moveInput = Vector2.zero;

        _inputs.GamePlay.Jump.performed -= OnJumpPerform;

        _inputs.GamePlay.Roll.performed -= OnRollPerform;

        _inputs.Disable();
    }

    /// <summary>
    /// Кувырок
    /// </summary>
    private void Roll()
    {
        _player.EnableRollingAnimationState();
    }

    /// <summary>
    /// Движение
    /// </summary>
    private void Move()
    {
        Flip();

        if (_moveInput.x != 0 && !_player.IsTouchingWall)
        {
            _rb.velocity = new Vector2(_moveInput.x * _speed, _rb.velocity.y);
        }
        else if (_player.IsTouchingWall && Mathf.Sign(_moveInput.x) == Mathf.Sign(_rb.velocity.x))
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
    }

    /// <summary>
    /// Движение по лестницам
    /// </summary>
    private void StairsMove()
    {
        Flip();

        _rb.velocity = new Vector2(_moveInput.x * _speed, _moveInput.y * _speed);
    }

    private void RopeSwing()
    {
        _rb.AddForce(Vector2.right * _moveInput.x * swingForce);
    }

    /// <summary>
    /// Прыжок
    /// </summary>
    private void Jump()
    {
        if (!_player.IsOnRope)
        {
            _rb.drag = 1f;

            _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);

            _player.SetJumpingState(true);
        }
        else
            _player.DetachFromRope();
        
    }

    /// <summary>
    /// Поворот в сторону движения
    /// </summary>
    private void Flip()
    {
        if (_moveInput.x != 0)
        {
            if (_moveInput.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else 
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        } 
    }
}
