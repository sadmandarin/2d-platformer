using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RubyArrow : AbilitySOBase
{
    private Rigidbody2D _rb;
    private Vector2 _startPos;
    private Vector2 _direction;
    private float _speed = 9;
    private float _maxDistance = 13;
    private int _maxCountOfEnemy = 2;
    private int _currentCountEnemy = 0;

    public override void InitializeStats(Vector2 direction, Vector2 startPos)
    {
        _rb = GetComponent<Rigidbody2D>();

        _direction = direction;
        _startPos = startPos;

        StartCoroutine(StartAttacking());
    }

    protected override IEnumerator StartAttacking()
    {
        while (Vector2.Distance(_startPos, transform.position) <= _maxDistance)
        {
            _rb.velocity = new Vector2(_direction.x * _speed, _rb.velocity.y);

            yield return new WaitForFixedUpdate();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyBase>())
        {
            _currentCountEnemy++;
            collision.gameObject.GetComponent<EnemyBase>().TakeDamage(Damage);
        }
        else if (collision.gameObject.GetComponent<BossBase>())
        {
            _currentCountEnemy++;
            collision.gameObject.GetComponent<BossBase>().TakeDamage(Damage, true);
        }

        if (_currentCountEnemy == _maxCountOfEnemy)
        {
            Destroy(gameObject);
        }
    }
}
