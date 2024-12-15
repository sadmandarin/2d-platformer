using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ����������� ����� ��� ���� ������
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
    /// ��� ��������� �����
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
    /// �����, ����������� ����������� �����
    /// </summary>
    protected abstract void HandleState();

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
    /// �������� � ������ ����� ������
    /// </summary>
    /// <param name="speed">�������� ���������</param>
    protected abstract void MoveTowardsPlayer(float speed, Vector2 destination);

    /// <summary>
    /// ��������� ��������
    /// </summary>
    protected abstract void IdleMove();

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
    public virtual void ArmorDamage(int damage, bool isRanged)
    {
        OnArmorTookDamage?.Invoke();

        Debug.Log("ArmorDamage");
    }

    /// <summary>
    /// �����, �������������� ����� ������ �� �����, ���� ��� �����
    /// </summary>
    /// <param name="damage">�����, ������� �������� �� ������</param>
    /// <param name="isRanged">��� ������, ������� ������ �����</param>
    public virtual void TakeDamage(int damage, bool isRanged)
    {
        OnHealthTookDamage?.Invoke();

        Debug.Log("HealthDamage");
    }

}
