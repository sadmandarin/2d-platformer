using System.Collections;
using UnityEngine;

/// <summary>
/// ��������� ����� �����
/// </summary>
public class DefaultEnemy : EnemyBase
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /// <summary>
    /// ������ ����������
    /// </summary>
    private void Die()
    {
        _isDead = true;
        _animator.SetTrigger("Death");

        StartCoroutine(DestroyEnemyOnDeath());
    }

    /// <summary>
    /// �������� ����� � �������� ���� ����� �������� ��������
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroyEnemyOnDeath()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    
    public override void TakeDamage(int damage)
    {
        Hp -= damage;

        if (Hp <= 0 && !_isDead)
        {
            Die();
        }

        Debug.Log($"�������� ����� ������: {damage}");
        Debug.Log($"�������� HP � �����: {Hp}");
    }

    protected override void MoveTowardsPlayer()
    {
        FlipX();

        if (IsPlayerDetected && _states != States.Attack)
        {
            if (IsOnGround)
            {
                Vector2 direction = (PlayerTransform.position - transform.position).normalized;

                _rb.velocity = new Vector2(direction.x * _speed, _rb.velocity.y);
            }
        }
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


    protected override void FlipX()
    {
        if (PlayerTransform != null)
        {
            if ((PlayerTransform.position - transform.position).normalized.x > 0)
                transform.localScale = new Vector3(1, 1, 1);

            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected override void PerformAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);

        AttackAnim();

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Player>())
            {
                var enemyCollider = enemy.GetComponent<Player>();

                if (!enemyCollider.IsRolling)
                    enemyCollider.TakeDamage(Damage, transform);
            }
        }

        Debug.Log("Attack by Sword " + Damage);
    }

    protected override void HandleState()
    {
        if (Time.time >= _lastTimeAttack + 1 / _attackSpeed && _states == States.Attack)
        {
            if (!_isAttacking)
            {
                _startAttackingTime = Time.time;
                _isAttacking = true;
                PerformAttack();
                _lastTimeAttack = Time.time;
            }
        }
    }
}
