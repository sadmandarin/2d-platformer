using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAttack : MonoBehaviour
{
    private Enemy _enemy;
    private float _attackSpeed = 2;
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
        if (!_enemy.IsDead)
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
        
        
    }

    void PerformAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);

        _enemy.AttackAnim();

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Player>())
            {
                var enemyCollider = enemy.GetComponent<Player>();

                if (!enemyCollider.IsRolling)
                    enemyCollider.TakeDamage(_enemy.Damage);
            }
        }

        Debug.Log("Attack by Sword " + _enemy.Damage);
    }

    private void OnAttackAnimationComplete()
    {
        _isAttacking = false;
    }
}
