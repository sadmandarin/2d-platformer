using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyAttack : MonoBehaviour
{
    private Enemy _enemy;
    private float _attackSpeed = 1;
    private float _lastTimeAttack;
    private float _startAttackingTime;
    private bool _isAttacking = false;

    [SerializeField] private float _attackRange;
    [SerializeField] private Transform _attackPoint;

    private void Awake()
    {
        _lastTimeAttack = Time.time;
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (Time.time >= _lastTimeAttack + 1 / _attackSpeed && _enemy.CanAttack)
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

    void PerformAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);

        _enemy.AttackAnim();

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Player>())
                enemy.GetComponent<Player>().TakeDamage(_enemy.Damage);
        }

        _isAttacking = false;


        Debug.Log("Attack by Sword " + _enemy.Damage);

    }
}
