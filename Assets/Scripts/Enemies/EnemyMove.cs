using UnityEngine;

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
        _rb.gravityScale = (_enemy.IsOnStairs) ? 0 : 1;

        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (_enemy.IsPlayerDetected)
        {
            if (_enemy.IsOnGround)
            {
                Vector2 direction = (_enemy.PlayerTransform.position - transform.position).normalized;

                _rb.velocity = new Vector2(direction.x * _speed, _rb.velocity.y);
            }
        }
    }
}
