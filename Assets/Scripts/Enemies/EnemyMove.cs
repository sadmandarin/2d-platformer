using UnityEngine;

[RequireComponent (typeof(Enemy), typeof(Rigidbody2D))]
public class EnemyMove : MonoBehaviour
{
    private int _speed = 3;
    private int _jump;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!_enemy.IsDead)
        {
            _enemy.SetMovingState(_rb.velocity.x == 0 ? 0 : 2);

            if (_enemy.IsOnStairs)
            {
                _rb.gravityScale = 0;

                StairsMove();
            }

            else
            {
                _rb.gravityScale = 1;

                MoveTowardsPlayer();
            }
            
        }
    }

    private void MoveTowardsPlayer()
    {
        FlipX();

        if (_enemy.IsPlayerDetected && !_enemy.CanAttack)
        {
            if (_enemy.IsOnGround)
            {
                Vector2 direction = (_enemy.PlayerTransform.position - transform.position).normalized;

                _rb.velocity = new Vector2(direction.x * _speed, _rb.velocity.y);
            }
        }
    }

    private void StairsMove()
    {
        FlipX();

        if (_enemy.IsPlayerDetected)
        {
            Vector2 direction = (_enemy.PlayerTransform.position - transform.position).normalized;

            _rb.velocity = new Vector2(direction.x * _speed, direction.y * _speed);
        }
    }

    private void FlipX()
    {
        if (_enemy.PlayerTransform != null)
        {
            if ((_enemy.PlayerTransform.position - transform.position).normalized.x > 0)
                transform.rotation = Quaternion.Euler(0, 180, 0);

            else
                transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
    }
}
