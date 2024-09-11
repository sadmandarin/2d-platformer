using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _hp = 100;
    [SerializeField] private int _damage;
    [SerializeField] private bool _isOnGround;
    [SerializeField] private int _isMoving;
    private bool _isObstacleAhead;
    private bool _canAttack;
    private bool _isOnStairs;
    private bool _isDead = false;
    [SerializeField] private bool _isPlayerDetected;
    private Transform _playerTransform;
    private Animator _animator;

    public float Hp { get { return _hp; } private set { _hp = value; } }
    public int Damage { get { return _damage; } private set { _damage = value; } }
    public bool IsOnGround { get { return _isOnGround; } private set { _isOnGround = value; } }
    public int IsMoving
    {
        get 
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
        }
    }
    public bool CanAttack
    {
        get { return _canAttack; }
        private set
        {
            _canAttack = value;
        }
    }
    public bool IsObstacleAhead { get { return _isObstacleAhead; } private set { _isObstacleAhead = value; } }
    public bool IsDead { get { return _isDead; } private set { _isDead = value; } }
    public bool IsOnStairs { get { return _isOnStairs; } private set { _isOnStairs = value; } }
    public bool IsPlayerDetected { get { return _isPlayerDetected; } private set { _isPlayerDetected = value; } }
    public Transform PlayerTransform { get { return _playerTransform; } private set { _playerTransform = value; } }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        IsOnStairs = false;
    }

    public bool SetPlayerDetection(bool detect)
    {
        return IsPlayerDetected = detect;
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        PlayerTransform = playerTransform;
    }

    public bool SetAttackState(bool canAttack)
    {
        return CanAttack = canAttack;
    }

    public void AttackAnim()
    {
        _animator.SetTrigger("Attack");
    }

    public void IsTouchedStairs(bool isTouchedStairs)
    {
        IsOnStairs = isTouchedStairs;
    }

    public int SetMovingState(int state)
    {
        return IsMoving = state;
    }

    public void GroundChecker(bool touchedGround)
    {
        IsOnGround = touchedGround;
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;

        if (Hp <= 0 && !_isDead)
        {
            Die();
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

    private IEnumerator DestroyEnemyOnDeath()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
