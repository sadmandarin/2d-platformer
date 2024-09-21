using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// ����������� ����� ��� ���� ������
/// </summary>
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

    public event Action BossDead;

    /// <summary>
    /// ��� ��������� �����
    /// </summary>
    public enum BossState 
    {
        Idle,
        MeleeAttack,
        ComboMeleeAttack,
        SpecialAttack,
        Block,
        Retreat
    }


    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _state = BossState.Idle;
        InitializeStats();
    }

    protected virtual void Update()
    {
        if (_player != null)
        {
            HandleState();
        }
    }

    /// <summary>
    /// �����, ����������� ����������� �����
    /// </summary>
    protected void HandleState()
    {
        switch (_state)
        {
            case BossState.Idle:
                Idle();
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

    [ContextMenu("GameWin")]
    /// <summary>
    /// �����������, ���� HP ����� ������ 0
    /// </summary>
    protected void Die()
    {
        _isDead = true;
        _animator.SetTrigger("Death");
        StartCoroutine(DestroyBossAfterDeath());
    }
    /// <summary>
    /// �����, ��������� ����� �� �����
    /// </summary>
    private IEnumerator DestroyBossAfterDeath()
    {
        yield return new WaitForSeconds(2f);

        GameWin();

        Destroy(gameObject);
    }

    private void GameWin()
    {
        BossDead?.Invoke();
    }

    /// <summary>
    /// �����, ��������������� ������ � �������� ���� ��� �����
    /// </summary>
    /// <param name="player">������ ������</param>
    public abstract void SetPlayer(Player player);

    /// <summary>
    /// �����, ����������� ��������� ��������� ������������� �����
    /// </summary>
    protected abstract void InitializeStats();

    /// <summary>
    /// �����, �������� ���������� ��������� �����
    /// </summary>
    protected abstract IEnumerator AttackInterval();

    /// <summary>
    /// �����, �������������� ����� ������ �� �����, ���� ��� �����
    /// </summary>
    /// <param name="damage">�����, ������� �������� �� ������</param>
    /// <param name="isRanged">��� ������, ������� ������ �����</param>
    public abstract void TakeDamage(int damage, bool isRanged);

    /// <summary>
    /// �������� � ������
    /// </summary>
    protected abstract void MoveTowardsPlayer();

    /// <summary>
    /// ��������� ��������
    /// </summary>
    protected abstract void Idle();

    /// <summary>
    /// ������ ��������� � �������
    /// </summary>
    protected abstract void Retreat();

    /// <summary>
    /// ����� �����
    /// </summary>
    public abstract void Attack();

    /// <summary>
    /// ������� ����� �����
    /// </summary>
    protected abstract void ComboMeleeAttack(); 

    /// <summary>
    /// ����� ����������� �����
    /// </summary>
    public abstract void SpecialAttack();

    /// <summary>
    /// ����� �����
    /// </summary>
    public abstract void Block();

    /// <summary>
    /// �����, �������������� ����� ������,���� ���� �����
    /// </summary>
    /// <param name="damage">����, ���������� �� ������</param>
    /// <param name="isRanged">��� ������, ������� ����� ���� �����</param>
    public abstract void ArmorDamage(int damage, bool isRanged);
}
