using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Абстрактный класс для всех боссов
/// </summary>
[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public abstract class BossBase : MonoBehaviour
{
    [SerializeField] protected int _baseDamage;
    [SerializeField] protected int _specialAttackDamage;
    [SerializeField] protected int _baseHealth;
    [SerializeField] protected int _baseMaxHealth;
    protected float _baseSpeed;
    [SerializeField] protected int _helmetArmorHealth;
    [SerializeField] protected int _bodyArmorHealth;
    [SerializeField] protected int _allArmorHealth;
    protected float _reduceDamageByBlock = 0.7f;


    protected bool _isBlocking;
    protected bool _isPlayerDetected;
    protected bool _isDead = false;


    [SerializeField] protected Transform _attackPointTransform;
    protected Animator _animator;
    protected Player _player;
    protected Transform _playerPosition;
    protected Rigidbody2D _rb;
    protected BossState _state;

    public Bestiary Bestiary;

    public event Action BossDead;
    public event Action OnArmorTookDamage;
    public event Action OnHealthTookDamage;

    public int Health { get { return _baseHealth; } }
    public int MaxHealth {  get { return _baseMaxHealth; } }
    public int AllArmorHealth { get { return _allArmorHealth; } }
    public int HelmetArmor { get { return _helmetArmorHealth; } }
    public int BodyArmor {  get { return _bodyArmorHealth; } }
    /// <summary>
    /// Все состояния босса
    /// </summary>
    public enum BossState 
    {
        Idle,
        MeleeAttack,
        ComboMeleeAttack,
        SpecialAttack,
        Retreat,
        TrapDeploy
    }


    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _state = BossState.Idle;
        InitializeStats();
    }

    private void OnEnable()
    {
        GameManager.Instance.InitializeComponent(this);
    }

    protected virtual void Update()
    {
        if (_player != null)
        {
            HandleState();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (_attackPointTransform == null)
            return;

        Gizmos.DrawWireSphere(_attackPointTransform.position, 1.7f);
    }

    protected void OnTakeBestiary()
    {
        Debug.Log(Bestiary.Name);
        Debug.Log(Bestiary.Description);

        NotesAndBestiaryManager.Instance.AddToReadsBestiary(Bestiary);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    /// <summary>
    /// Метод, управляющий состояниями босса
    /// </summary>
    protected abstract void HandleState();

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
    /// Движение к игроку перед атакой
    /// </summary>
    /// <param name="speed">Скорость сближения</param>
    protected abstract void MoveTowardsPlayer(float speed, Vector2 destination);

    /// <summary>
    /// Состояние ожидания
    /// </summary>
    protected abstract void IdleMove();

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
    public virtual void ArmorDamage(int damage, bool isRanged)
    {
        OnArmorTookDamage?.Invoke();

        Debug.Log("ArmorDamage");
    }

    /// <summary>
    /// Метод, обрабатывающий удары игрока по боссу, если нет брони
    /// </summary>
    /// <param name="damage">Дамаг, который приходит от игрока</param>
    /// <param name="isRanged">Тип оружия, которым ударил игрок</param>
    public virtual void TakeDamage(int damage, bool isRanged)
    {
        OnHealthTookDamage?.Invoke();

        Debug.Log("HealthDamage");
    }

}
