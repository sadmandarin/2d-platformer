using UnityEngine;

/// <summary>
/// Класс движения игрока
/// </summary>
[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _wallCheck;

    private float _horizontalInput;
    private float _verticalInput;

    private string _horizontalAxis = "Horizontal";
    private string _verticalAxis = "Vertical";

    private Rigidbody2D _rb;
    private Player _player;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (!_player.IsStunned)
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

            if (Input.GetKeyDown(KeyCode.Space) && _player.IsOnGround && !_player.IsRolling)
            {
                Jump();
            }
        }

        else
            _rb.velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (!_player.IsStunned)
        {
            _player.SetFallingState(_rb.velocity.y);
            _player.SetMovingState(_horizontalInput == 0 ? 0 : 1);
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

        if (_horizontalInput != 0 && !_player.IsTouchingWall)
        {
            _rb.velocity = new Vector2(_horizontalInput * _speed, _rb.velocity.y);
        }
        else if (_player.IsTouchingWall && Mathf.Sign(_horizontalInput) == Mathf.Sign(_rb.velocity.x))
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

        _rb.velocity = new Vector2(_horizontalInput * _speed, _verticalInput * _speed);
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
        if (_horizontalInput != 0)
        {
            if (_horizontalInput > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else 
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        } 
    }
}
