using System.Collections;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float _hp;
    [SerializeField] protected float _speed;
    [SerializeField] protected int _damage;
    [SerializeField] protected bool _isOnGround;
    [SerializeField] protected int _isMoving;
    [SerializeField] protected float _attackSpeed;
    private float _idleMoveSpeed = 1.5f;

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
    protected bool _isIdleCoroutineActive = false;
    protected States _states = States.Idle;

    [SerializeField] protected float _attackRange;
    [SerializeField] protected Transform _attackPoint;
    [SerializeField] protected Vector2 _startPos;

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
        Chase,
        Retreat
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _startPos = transform.position;

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
        if (!IsDead && _states != States.Chase)
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

        if (_states == States.Idle)
        {
            IdleMove();
        }
    }

    /// <summary>
    /// �����, ���������� ����� ���������� �������� �����
    /// </summary>
    private void OnAttackAnimationComplete()
    {
        _isAttacking = false;
    }

    /// <summary>
    /// ����������� ������
    /// </summary>
    /// <param name="detect"></param>
    /// <returns></returns>
    public bool SetPlayerDetection(bool detect)
    {
        return IsPlayerDetected = detect;
    }

    /// <summary>
    /// ��������� ������� ������ ��� �������������
    /// </summary>
    /// <param name="playerTransform">Transform ������</param>
    public void SetPlayerTransform(Transform playerTransform)
    {
        PlayerTransform = playerTransform;
    }

    /// <summary>
    /// ��������� ����� ����������� �����
    /// </summary>
    /// <param name="canAttack"></param>
    /// <returns></returns>
    public void SetAttackState(bool canAttack)
    {
        if (canAttack)
        {
            StopCoroutine(IEIdleMove());
            _states = States.Attack;
        }

        else if (_isPlayerDetected)
            _states = States.Chase;
    }

    /// <summary>
    /// �������� �����
    /// </summary>
    protected void AttackAnim()
    {
        _animator.SetTrigger("Attack");
    }

    /// <summary>
    /// �������� ������� �������� ��������
    /// </summary>
    /// <param name="isTouchedStairs"></param>
    public void IsTouchedStairs(bool isTouchedStairs)
    {
        IsOnStairs = isTouchedStairs;
    }

    /// <summary>
    /// ��������� ��������� ��������
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public int SetMovingState(int state)
    {
        return IsMoving = state;
    }

    /// <summary>
    /// ��������� ����� ��������������� � ������
    /// </summary>
    /// <param name="touchedGround"></param>
    public void GroundChecker(bool touchedGround)
    {
        IsOnGround = touchedGround;
    }

    /// <summary>
    /// ��������� ����� �������
    /// </summary>
    /// <param name="damage">���������� ����������� �����</param>
    public abstract void TakeDamage(int damage);

    /// <summary>
    /// �������� � ������
    /// </summary>
    protected abstract void MoveTowardsPlayer();

    /// <summary>
    /// ������������ �� ���������
    /// </summary>
    protected abstract void StairsMove();

    /// <summary>
    /// ������� � ������� ������
    /// </summary>
    protected abstract void FlipX();

    /// <summary>
    /// �����
    /// </summary>
    protected abstract void PerformAttack();

    /// <summary>
    /// ������������ �����������
    /// </summary>
    protected abstract void HandleState();

    /// <summary>
    /// �������� �� ����������� ������
    /// </summary>
    protected void IdleMove()
    {
        if (!_isIdleCoroutineActive && _isOnGround)
        {
            StartCoroutine(IEIdleMove());
        }
    }

    private IEnumerator IEIdleMove()
    {
        bool forward = transform.localScale.x > 0 ? true : false;
        _isIdleCoroutineActive = true;
        SetMovingState(2);
        while (true)
        {
            if (forward)
            {
                _rb.velocity = new Vector2(_idleMoveSpeed, _rb.velocity.y);

                if ((transform.position.x  >= _startPos.x + 3))
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);

                    forward = false;
                }
            }

            else
            {
                _rb.velocity = new Vector2(-_idleMoveSpeed, _rb.velocity.y);

                if ((transform.position.x <= _startPos.x - 3))
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);

                    forward = true;
                }
            }

            yield return null;
        }
    }
}
