using System.Collections;
using UnityEngine;

/// <summary>
/// Класс дальнего врага
/// </summary>
public class LongRangeEnemy : EnemyBase
{
    [SerializeField] private GameObject _arrowPrefab;

    private float _distance = 8;
    private int _hitCount = 0;

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!IsDead && _states != States.Attack)
        {
            SetMovingState(_rb.velocity.x == 0 ? 0 : 2);

            if (IsOnStairs)
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
        else if (_states == States.Attack)
        {
            SetMovingState(0);
        }
    }

    public override void TakeDamage(int damage)
    {
        Hp -= damage;

        if (Hp <= 0 && !_isDead)
        {
            Die();
        }

        _hitCount++;

        if (_hitCount == 2)
        {
            _states = States.Retreat;
            _hitCount = 0;
        }

        Debug.Log($"Получено урона врагом: {damage}");
        Debug.Log($"Осталось HP у врага: {Hp}");
    }

    private void Die()
    {
        _isDead = true;
        _animator.SetTrigger("Death");

        StartCoroutine(DestroyEnemyOnDeath());
    }

    /// <summary>
    /// Удаление врага с игрового поля через заданный интервал
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroyEnemyOnDeath()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    protected override void FlipX()
    {
        if (PlayerTransform != null)
        {
            if ((PlayerTransform.position - transform.position).normalized.x > 0)
                transform.localScale = new Vector3(2, 2, 2);

            else
                transform.localScale = new Vector3(-2, 2, 2);
        }
    }

    protected override void HandleState()
    {
        switch (_states)
        {
            case States.Idle:
                IdleMove();
                break;
            case States.Attack:
                AttackState();
                break;
            case States.Retreat:
                Retreat();
                break;
        }
    }

    protected void AttackState()
    {
        if (Time.time >= _lastTimeAttack + 1 / _attackSpeed && _states == States.Attack)
        {
            if (!_isAttacking)
            {
                _startAttackingTime = Time.time;
                PerformAttack();
            }
        }
    }

    private void IdleMove()
    {

    }

    protected override void MoveTowardsPlayer()
    {
        FlipX();

        if (IsPlayerDetected && _states != States.Attack)
        {
            if (IsOnGround)
            {
                if (Mathf.Abs(_playerTransform.position.x - transform.position.x) >= _distance)
                {
                    Vector2 direction = (PlayerTransform.position - transform.position).normalized;

                    _rb.velocity = new Vector2(direction.x * _speed, _rb.velocity.y);
                }
            }
        }
    }

    protected override void PerformAttack()
    {
        if (Time.time >= _lastTimeAttack + 1 / _attackSpeed)
        {
            if (!_isAttacking)
            {
                AttackAnim();
            }
        }
    }

    private void Shoot()
    {
        _startAttackingTime = Time.time;
        _isAttacking = true;

        GameObject arrow = Instantiate(_arrowPrefab, _attackPoint.position, Quaternion.identity);

        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        arrow.GetComponent<EnemyLongRangeAttackArrow>().Initialize(direction, _damage);

        _lastTimeAttack = Time.time;
    }

    protected override void StairsMove()
    {
        FlipX();

        if (IsPlayerDetected)
        {
            Vector2 direction = (PlayerTransform.position - transform.position).normalized;

            _rb.velocity = new Vector2(direction.x * _speed, direction.y * _speed);
        }
    }

    private void Retreat()
    {
        Debug.Log("Убегаем");

        Vector2 direction = (_playerTransform.position - transform.position).normalized;

        _rb.velocity = new Vector2(-direction.x * _speed * 2, _rb.velocity.y);

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(2, 2, 2);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }

        if (Mathf.Abs(_playerTransform.position.x - transform.position.x) >= _distance)
        {
            _states = States.Attack;
        }
    }
}
