using System;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Animator), typeof(Rigidbody2D))]
public abstract class BossBase : MonoBehaviour
{
    protected int _baseDamage;
    protected int _specialAttackDamage;
    protected int _baseHealth;
    protected float _baseSpeed;
    protected float _reduceDamageByBlock = 0.7f;


    protected int _currenHp;
    protected bool _isBlocking;
    protected bool _isPlayerDetected;
    protected bool _isDead = false;

    protected Animator _animator;
    protected Player _player;
    protected Transform _playerPosition;
    protected Rigidbody2D _rb;
    protected BossState _state;

    public enum BossState 
    {
        NoAction,
        Idle,
        MeleeAttack,
        ComboMeleeAttack,
        SpecialAttack,
        Block,
        Retreat
    }


    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();

        _state = BossState.Idle;

        InitializeStats();
    }

    protected virtual void Update()
    {
        HandleState();

        if (_player != null)
        {
            if (Vector2.Distance(transform.position, _playerPosition.transform.position) >= 2)
            {
                if (_state != BossState.Idle && _state != BossState.Retreat)
                {
                    MoveTowardsPlayer();
                }
            }
        }
    }

    protected void HandleState()
    {
        switch (_state)
        {
            case BossState.Idle:
                break;
            case BossState.NoAction:
                break;
            case BossState.MeleeAttack:
                Attack();
                break;
            case BossState.ComboMeleeAttack:
                ComboMeleeAttack();
                break;
            case BossState.SpecialAttack:
                SpecialAttack();
                break;
            case BossState.Block:
                Block(); 
                break;
            case BossState.Retreat:
                Retreat();
                break;

        }
    }

    protected void Die()
    {
        _isDead = true;
        _animator.SetTrigger("Death");
        StartCoroutine(DestroyBossAfterDeath());
    }

    private IEnumerator DestroyBossAfterDeath()
    {
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
    public abstract void SetPlayer(Player player);
    protected abstract void InitializeStats();
    protected abstract IEnumerator AttackPattern();

    public abstract void TakeDamage(int damage, bool isRanged);
    protected abstract void MoveTowardsPlayer();
    protected abstract void Retreat();
    public abstract void Attack();
    protected abstract IEnumerator ComboMeleeAttack(); 
    public abstract void SpecialAttack();
    public abstract void Block();
    public abstract void ArmorDamage(int damage, bool isRanged);
}
