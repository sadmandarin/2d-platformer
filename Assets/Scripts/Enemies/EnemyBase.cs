using Cinemachine.Editor;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float _hp;
    [SerializeField] protected float _speed;
    [SerializeField] protected int _damage;
    [SerializeField] protected bool _isOnGround;
    [SerializeField] protected int _isMoving;
    [SerializeField] protected float _attackSpeed;

    protected bool _isObstacleAhead;
    [SerializeField] protected bool _canAttack;
    protected bool _isOnStairs;
    [SerializeField] protected bool _isDead = false;
    [SerializeField] protected bool _isPlayerDetected;
    protected Transform _playerTransform;
    protected Animator _animator;
    protected Rigidbody2D _rb;
    protected float _lastTimeAttack;
    protected float _startAttackingTime;
    protected bool _isAttacking = false;
    protected States _states = States.Idle;

    [SerializeField] protected float _attackRange;
    [SerializeField] protected Transform _attackPoint;

    public float Hp { get { return _hp; } protected set { _hp = value; } }
    public int Damage { get { return _damage; } protected set { _damage = value; } }
    public bool IsOnGround { get { return _isOnGround; } protected set { _isOnGround = value; } }
    public int IsMoving
    {
        get
        {
            return _isMoving;
        }
        protected set
        {
            _isMoving = value;
            _animator.SetInteger("AnimState", value);
        }
    }
    public bool CanAttack
    {
        get { return _canAttack; }
        protected set
        {
            _canAttack = value;
        }
    }
    public bool IsObstacleAhead { get { return _isObstacleAhead; } protected set { _isObstacleAhead = value; } }
    public bool IsDead { get { return _isDead; } protected set { _isDead = value; } }
    public bool IsOnStairs { get { return _isOnStairs; } protected set { _isOnStairs = value; } }
    public bool IsPlayerDetected { get { return _isPlayerDetected; } protected set { _isPlayerDetected = value; } }
    public Transform PlayerTransform { get { return _playerTransform; } protected set { _playerTransform = value; } }

    protected enum States
    {
        Idle,
        Attack, 
        Retreat
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();

        _lastTimeAttack = Time.time;
        IsOnStairs = false;
    }

    protected virtual void Update()
    {
        if (!IsDead)
        {
            HandleState();
        }
    }

    protected virtual void FixedUpdate()
    {
        
    }

    /// <summary>
    /// Метод, вызываемый после завершения анимации атаки
    /// </summary>
    private void OnAttackAnimationComplete()
    {
        _isAttacking = false;
    }

    /// <summary>
    /// Обнаружение игрока
    /// </summary>
    /// <param name="detect"></param>
    /// <returns></returns>
    public bool SetPlayerDetection(bool detect)
    {
        return IsPlayerDetected = detect;
    }

    /// <summary>
    /// Установка позиции игрока для преследования
    /// </summary>
    /// <param name="playerTransform">Transform игрока</param>
    public void SetPlayerTransform(Transform playerTransform)
    {
        PlayerTransform = playerTransform;
    }

    /// <summary>
    /// Установка флага возможности атаки
    /// </summary>
    /// <param name="canAttack"></param>
    /// <returns></returns>
    public void SetAttackState(bool canAttack)
    {
        if (canAttack)
        {
            _states = States.Attack;
        }
            
        //else
        //    _states = States.Idle;
    }

    /// <summary>
    /// Анимация атаки
    /// </summary>
    protected void AttackAnim()
    {
        _animator.SetTrigger("Attack");
    }

    /// <summary>
    /// Проверка касания триггера лестницы
    /// </summary>
    /// <param name="isTouchedStairs"></param>
    public void IsTouchedStairs(bool isTouchedStairs)
    {
        IsOnStairs = isTouchedStairs;
    }

    /// <summary>
    /// Установка состояния движения
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public int SetMovingState(int state)
    {
        return IsMoving = state;
    }

    /// <summary>
    /// Установка флага соприкосновения с землей
    /// </summary>
    /// <param name="touchedGround"></param>
    public void GroundChecker(bool touchedGround)
    {
        IsOnGround = touchedGround;
    }

    /// <summary>
    /// Нанесение урона игроком
    /// </summary>
    /// <param name="damage">Количество нанесенного урона</param>
    public abstract void TakeDamage(int damage);

    /// <summary>
    /// Движение к игроку
    /// </summary>
    protected abstract void MoveTowardsPlayer();

    /// <summary>
    /// Передвижение по лестницам
    /// </summary>
    protected abstract void StairsMove();

    /// <summary>
    /// Поворот в сторону игрока
    /// </summary>
    protected abstract void FlipX();

    /// <summary>
    /// Атака
    /// </summary>
    protected abstract void PerformAttack();

    /// <summary>
    /// Управлениями состояниями
    /// </summary>
    protected abstract void HandleState();
}
