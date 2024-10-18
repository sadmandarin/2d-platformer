using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

/// <summary>
/// Класс, описывающий основные характеристики игрока
/// </summary>
[RequireComponent(typeof(Animator), typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _playerHp;
    [SerializeField] private float _playerMp;

    [SerializeField] private bool _isOnGround = true;
    private bool _isOnStair;
    private bool _isInStelsMode;
    private float _isFalling;
    [SerializeField] private bool _isJumping;
    [SerializeField] private bool _isRolling;
    [SerializeField] private bool _isBlocking;
    [SerializeField] private bool _isRollingAnimationStart;
    private bool _isTouchingWall;
    [SerializeField] private int _isMoving;
    private bool _isStunned = false;
    private int _quickAttackState = 0;


    [SerializeField] private WeaponsBase _currentMeleeWeapon;
    [SerializeField] private GameObject _currentLongRangeWeapon;
    [SerializeField] private GameObject _currentAbility;
    [SerializeField] private PlayerArmorBase _currentArmor;
    [SerializeField] private HealthRecovery _currentHealingItem;
    [SerializeField] private ManaRecovery _currentManaRecoveryItem;
    [SerializeField] private PlayerSettingsSO _playerSettingsSO;
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;
    private Inputs _inputs;
    private PlayerAttack _playerAttack;

    public int MaxHP = 100;
    public int MaxMP = 100;
    private bool _isFacingRight = true;

    public event Action OnHPChanged;
    public event Action OnMPChanged;
    public event Action OnDied;

    public WeaponsBase CurrentMeleeWeapon { get { return _currentMeleeWeapon; } private set { _currentMeleeWeapon = value; } }

    public GameObject CurrentLongRangeWeapon { get { return _currentLongRangeWeapon; } private set { _currentLongRangeWeapon = value; } }
    
    public GameObject CurrentAbility { get { return _currentAbility; } private set { _currentAbility = value; } }
    public PlayerArmorBase CurrentArmor { get { return _currentArmor; } private set { _currentArmor = value; } }

    public bool IsJumping 
    {
        get
        {
            return _isJumping; 
        }
        private set
        {
            _isJumping = value;
            _animator.SetTrigger("Jump");
        }
    }

    public bool IsRolling { get { return _isRolling; } private set { _isRolling = value; } }

    public bool IsRollingAnimationStart { get { return _isRollingAnimationStart; } private set { _isRollingAnimationStart = value; } }

    public int IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;

            _animator.SetInteger("AnimState", value);
        }
    }

    public float IsFalling
    {
        get
        {
            return _isFalling;
        }
        private set
        {
            _isFalling = value;
            _animator.SetFloat("AirSpeedY", value);
        }
    }

    public float PlayerHp 
    { 
        get
        {
            return _playerHp;
        }
        private set 
        { 
            _playerHp = value;
            if (_playerHp <= 0)
            {
                Die();
            }
        } 
    }

    public float PlayerMp
    {
        get { return _playerMp; }   
        private set { _playerMp = value;}
    }

    public bool IsOnGround {
        get
        {
            return _isOnGround; 
        } 
        private set 
        {
            _isOnGround = value;

            _animator.SetBool("Grounded", value);
        }
    }
    public bool IsOnStair { get {  return _isOnStair; } private set { _isOnStair = value; } }
    public bool IsInStelsMode { get {  return _isInStelsMode; } private set { _isInStelsMode = value; } }
    public bool IsTouchingWall { get { return _isTouchingWall; } private set { _isTouchingWall = value; } }
    public bool IsStunned
    {
        get 
        {
            return _isStunned;
        }
        private set
        {
            _isStunned = value;
            //Сделать анимацию стана и запускать ее здесь
            //выходить из состояния стана по ивенту в конце анимации
        }
    }

    public bool IsBlocking { get { return _isBlocking; } private set { _isBlocking = value; } }

    public int QuickAttackState
    {
        get
        {
            return _quickAttackState;
        }
        private set
        {
            _quickAttackState = value;

            if (value % 2 == 0)
                _animator.SetTrigger("Attack1");
            else
                _animator.SetTrigger("Attack2");
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _inputs = new Inputs();
        _playerAttack = GetComponent<PlayerAttack>();

        SetHaracteristicOnStart();
    }

    private void OnEnable()
    {
        _inputs.GamePlay.Block.performed += OnBlockPerform;
        _inputs.GamePlay.Block.canceled += OnBlockCanceled;

        _inputs.GamePlay.Healing.performed += OnHeal;
        _inputs.GamePlay.ManaRecovery.performed += OnManaRecovery;

        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.GamePlay.Block.canceled -= OnBlockPerform;
        _inputs.GamePlay.Block.canceled -= OnBlockCanceled;

        _inputs.GamePlay.Healing.performed -= OnHeal;
        _inputs.GamePlay.ManaRecovery.performed -= OnManaRecovery;

        _inputs.Disable();
    }

    private void OnBlockPerform(InputAction.CallbackContext context)
    {
        _isBlocking = true;
        _animator.SetBool("IdleBlock", _isBlocking);
    }

    private void OnBlockCanceled(InputAction.CallbackContext context)
    {
        _isBlocking = false;
        _animator.SetBool("IdleBlock", _isBlocking);
    }

    /// <summary>
    /// Проверка соприкосновения с землей
    /// </summary>
    /// <param name="isOnGround"></param>
    /// <returns></returns>
    public bool GroundChecker(bool isOnGround)
    {
        _isJumping = false;
        return IsOnGround = isOnGround;
    }

    /// <summary>
    /// Проверка соприкосновения со стенами
    /// </summary>
    /// <param name="touch"></param>
    public void TouchedWall(bool touch)
    {
        IsTouchingWall = touch;
    }

    /// <summary>
    /// Установка состояния движения
    /// </summary>
    /// <param name="movingState"></param>
    /// <returns></returns>
    public int SetMovingState(int movingState)
    {
        return IsMoving = movingState;
    }

    /// <summary>
    /// Состояние падения
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float SetFallingState(float value)
    {
        return IsFalling = value;
    }

    /// <summary>
    /// Состояние прыжка
    /// </summary>
    /// <param name="jumpingState"></param>
    /// <returns></returns>
    public bool SetJumpingState(bool jumpingState)
    {
        return IsJumping = jumpingState;
    }

    /// <summary>
    /// Состояние кувырка, уменьшение коллайдера
    /// </summary>
    public void EnableRollingAnimationState()
    {
        _animator.SetTrigger("Roll");

        _boxCollider2D.offset = new Vector2(0, 0.36f);
        _boxCollider2D.size = new Vector2(0.73f, 0.6f);

        IsRollingAnimationStart = true;
    }

    /// <summary>
    /// Отключение состояния кувырка, возврат нормальных размеров коллайдеров
    /// </summary>
    public void DisableRollingAnimationState()
    {
        IsRollingAnimationStart = false;

        _boxCollider2D.offset = new Vector2(0, 0.662f);
        _boxCollider2D.size = new Vector2(0.73f, 1.2f);
    }

    /// <summary>
    /// Включение состояния кувырка через AnimationEvent
    /// </summary>
    public void SetRollingState()
    {
        IsRolling = true;
    }

    /// <summary>
    /// Отключение состояния кувырка через AnimationEvent
    /// </summary>
    public void ResetRollingState()
    {
        IsRolling = false;
    }

    public void SetStunState()
    {
        IsStunned = true;

        StartCoroutine(DisableStunState());
    }

    private IEnumerator DisableStunState()
    {
        yield return new WaitForSeconds(2);

        IsStunned = false;
    }

    /// <summary>
    /// Состояние быстрой атаки
    /// </summary>
    /// <param name="quickAttackState"></param>
    /// <returns></returns>
    public int SetQuickAttackState(int quickAttackState)
    {
        return QuickAttackState = quickAttackState;
    }

    public void SetStrongAttack()
    {
        _animator.SetTrigger("Attack3");
    }

    public void SetRecoveryItem(RecoveryItemBase recoveryItem)
    {
        if (recoveryItem is HealthRecovery recovery)
        {
            _currentHealingItem = recovery;
        }

        else if (recoveryItem is ManaRecovery manaRecovery)
        {
            _currentManaRecoveryItem = manaRecovery;
        }
    }

    /// <summary>
    /// Проверка касания лестниц
    /// </summary>
    /// <param name="isOnStair"></param>
    public void IsTouchedStairs(bool isOnStair)
    {
        IsOnStair = isOnStair;
    }

    /// <summary>
    /// Прием урона от противников
    /// </summary>
    /// <param name="damage">Количество урона</param>
    public void TakeDamage(int damage, Transform enemyPosition)
    {
        if (_isBlocking && !IsAttackFromBehind(enemyPosition))
        {
            PlayerHp -= damage * 0.5f;
            _animator.SetBool("Block", true);
            _animator.SetBool("IdleBlock", false);
            Debug.Log("Took damage" + damage);
        }
        else
        {
            PlayerHp -= damage;
            Debug.Log("TookD amage" + damage);
        }

        OnHPChanged?.Invoke();
    }

    private bool IsAttackFromBehind(Transform enemyPos)
    {
        Vector2 directionToEnemy = (enemyPos.position - transform.position).normalized;

        bool isFacingRight = transform.localScale.x > 0;

        Vector2 playerForward = isFacingRight ? Vector2.right : Vector2.left;

        // Вычисляем угол между направлением игрока и врага
        float angle = Vector2.Angle(playerForward, directionToEnemy);

        // Если угол больше 90 градусов, значит враг атакует сзади
        return angle > 90;
    }

    public void OnBlockAnimEnd()
    {
        _animator.SetBool("IdleBlock", _isBlocking);
        _animator.SetBool("Block", false);
    }

    /// <summary>
    /// Установка характеристик в начале уровня
    /// </summary>
    private void SetHaracteristicOnStart()
    {
        PlayerHp = _playerSettingsSO.Hp;
        PlayerMp = _playerSettingsSO.Mp;
        CurrentMeleeWeapon = _playerSettingsSO.MeleeWeapon;
        CurrentLongRangeWeapon = _playerSettingsSO.LongRangeWeapon;
        CurrentAbility = _playerSettingsSO.Ability;
        _playerAttack._activeAbility = CurrentAbility.GetComponent<AbilitySOBase>();
        CurrentArmor = _playerSettingsSO.PlayerArmor;
        MaxHP = 100;
        MaxMP = 100;
    }

    [ContextMenu("My method")]
    private void Die()
    {
        OnDied?.Invoke();
    }

    private void OnHeal(InputAction.CallbackContext context)
    {
        if (_currentHealingItem.ItemCount > 0)
        {
            if (PlayerHp <= MaxHP - _currentHealingItem.AmountOfRecovery)
            {
                PlayerHp += _currentHealingItem.AmountOfRecovery;
            }
            else
            {
                PlayerHp = MaxHP;
            }

            _currentHealingItem.ItemCount--;
            OnHPChanged?.Invoke();
        }
    }

    private void OnManaRecovery(InputAction.CallbackContext context)
    {
        Debug.Log("mana recovery");

        if (_currentManaRecoveryItem.ItemCount > 0)
        {
            if (PlayerMp <= MaxMP - _currentManaRecoveryItem.AmountOfRecovery)
            {
                PlayerHp += _currentHealingItem.AmountOfRecovery;
            }
            else
            {
                PlayerMp = MaxMP;
            }

            _currentManaRecoveryItem.ItemCount--;
            OnMPChanged?.Invoke();
        }
    }

    public void DecreaseMana(int mana)
    {
        Debug.Log("Decrease mana");

        if (PlayerMp >= mana)
            PlayerMp -= mana;
        else
            PlayerMp = 0;
    }
}
