using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Абстрактный класс для всех боссов
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
    /// Все состояния босса
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
    /// Метод, управляющий состояниями босса
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
    /// Выполняется, если HP босса меньше 0
    /// </summary>
    protected void Die()
    {
        _isDead = true;
        _animator.SetTrigger("Death");
        StartCoroutine(DestroyBossAfterDeath());
    }
    /// <summary>
    /// Метод, удаляющий босса со сцены
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
    /// Метод, устанавливающий игрока в качестве цели для врага
    /// </summary>
    /// <param name="player">Объект игрока</param>
    public abstract void SetPlayer(Player player);

    /// <summary>
    /// Метод, выполняющий первичную настройку характеристик босса
    /// </summary>
    protected abstract void InitializeStats();

    /// <summary>
    /// Метод, рандомно вызывающий состояния босса
    /// </summary>
    protected abstract IEnumerator AttackInterval();

    /// <summary>
    /// Метод, обрабатывающий удары игрока по боссу, если нет брони
    /// </summary>
    /// <param name="damage">Дамаг, который приходит от игрока</param>
    /// <param name="isRanged">Тип оружия, которым ударил игрок</param>
    public abstract void TakeDamage(int damage, bool isRanged);

    /// <summary>
    /// Движение к игроку
    /// </summary>
    protected abstract void MoveTowardsPlayer();

    /// <summary>
    /// Состояние ожидания
    /// </summary>
    protected abstract void Idle();

    /// <summary>
    /// Разрыв дистанции с игроком
    /// </summary>
    protected abstract void Retreat();

    /// <summary>
    /// Метод атаки
    /// </summary>
    public abstract void Attack();

    /// <summary>
    /// Ближняя комбо атака
    /// </summary>
    protected abstract void ComboMeleeAttack(); 

    /// <summary>
    /// Метод специальной атаки
    /// </summary>
    public abstract void SpecialAttack();

    /// <summary>
    /// Метод блока
    /// </summary>
    public abstract void Block();

    /// <summary>
    /// Метод, обрабатывающий удары игрока,если есть броня
    /// </summary>
    /// <param name="damage">Урон, приходящий от игрока</param>
    /// <param name="isRanged">Тип оружия, которым нанес удар игрок</param>
    public abstract void ArmorDamage(int damage, bool isRanged);
}
