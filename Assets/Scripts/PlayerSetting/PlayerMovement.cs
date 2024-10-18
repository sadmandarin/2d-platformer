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

    //private float _horizontalInput;
    //private float _verticalInput;

    //private string _horizontalAxis = "Horizontal";
    //private string _verticalAxis = "Vertical";

    private Rigidbody2D _rb;
    private Player _player;
    private Inputs _inputs;

    private bool _isJumping;
    private Vector2 _moveInput;
    private bool _jumpInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
        _inputs = new Inputs();
    }

    private void OnEnable()
    {
        //Actions
        _inputs.GamePlay.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _inputs.GamePlay.Move.canceled += ctx => _moveInput = Vector2.zero;

        _inputs.GamePlay.Jump.performed += OnJumpPerform;
        //_inputs.GamePlay.Jump.canceled += ctx => _jumpInput = false;

        _inputs.GamePlay.Roll.performed += OnRollPerform;

        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.GamePlay.Move.performed -= ctx => _moveInput = ctx.ReadValue<Vector2>();
        _inputs.GamePlay.Move.canceled -= ctx => _moveInput = Vector2.zero;

        _inputs.GamePlay.Jump.performed -= OnJumpPerform;
        //_inputs.GamePlay.Jump.canceled -= ctx => _jumpInput = false;

        _inputs.GamePlay.Roll.performed -= OnRollPerform;

        _inputs.Disable();
    }

    private void Update()
    {
        if (!_player.IsStunned)
        {
            
        }
        else
            _rb.velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (!_player.IsStunned)
        {
            _player.SetFallingState(_rb.velocity.y);
            _player.SetMovingState(_moveInput.x == 0 ? 0 : 1);
            if (!_player.IsBlocking)
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
            }
            else
                _rb.velocity = Vector2.zero;
            
        }
    }

    private void OnJumpPerform(InputAction.CallbackContext context)
    {
        if (!_player.IsStunned)
        {
            if (_player.IsOnGround && !_player.IsRolling)
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
        if (!_player.IsStunned)
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

    /// <summary>
    /// Прыжок
    /// </summary>
    private void Jump()
    {
        _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);

        _player.SetJumpingState(true);
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
