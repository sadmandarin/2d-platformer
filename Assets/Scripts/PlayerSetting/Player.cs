using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Animator), typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _playerHp;

    [SerializeField] private bool _isOnGround = true;
    private bool _isOnStair;
    private bool _isInStelsMode;
    private float _isFalling;
    [SerializeField] private bool _isJumping;
    [SerializeField] private bool _isRolling;
    [SerializeField] private bool _isRollingAnimationStart;
    private bool _isTouchingWall;
    [SerializeField] private int _isMoving;
    private int _quickAttackState = 0;


    [SerializeField] private WeaponsBase _currentMeleeWeapon;
    [SerializeField] private WeaponsBase _currentLongRangeWeapon;
    [SerializeField] private AbilitySOBase _currentAbility;
    [SerializeField] private PlayerArmorBase _currentArmor;
    [SerializeField] private PlayerSettingsSO _playerSettingsSO;
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;

    public int MaxHP = 100;
    public event Action OnTookDamage;

    public WeaponsBase CurrentMeleeWeapon { get { return _currentMeleeWeapon; } private set { _currentMeleeWeapon = value; } }

    public WeaponsBase CurrentLongRangeWeapon { get { return _currentLongRangeWeapon; } private set { _currentLongRangeWeapon = value; } }
    
    public AbilitySOBase CurrentAbility { get { return _currentAbility; } private set { _currentAbility = value; } }
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

    public float PlayerHp { get { return _playerHp; } private set { _playerHp = value; } }
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

    public int QuickAttackState
    {
        get
        {
            return _quickAttackState;
        }
        private set
        {
            _quickAttackState = value;

            if (value % 3 == 0)
                _animator.SetTrigger("Attack1");
            else if (value % 2 == 0)
                _animator.SetTrigger("Attack2");
            else
                _animator.SetTrigger("Attack3");
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        SetHaracteristicOnStart();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            TakeDamage(5);
        }
    }

    public bool GroundChecker(bool isOnGround)
    {
        return IsOnGround = isOnGround;
    }

    public void TouchedWall(bool touch)
    {
        IsTouchingWall = touch;
    }

    public int SetMovingState(int movingState)
    {
        return IsMoving = movingState;
    }

    public float SetFallingState(float value)
    {
        return IsFalling = value;
    }

    public bool SetJumpingState(bool jumpingState)
    {
        return IsJumping = jumpingState;
    }

    public void EnableRollingAnimationState()
    {
        _animator.SetTrigger("Roll");

        _boxCollider2D.offset = new Vector2(0, 0.36f);
        _boxCollider2D.size = new Vector2(0.73f, 0.6f);

        IsRollingAnimationStart = true;
    }

    public void DisableRollingAnimationState()
    {
        IsRollingAnimationStart = false;

        _boxCollider2D.offset = new Vector2(0, 0.662f);
        _boxCollider2D.size = new Vector2(0.73f, 1.2f);
    }

    public void SetRollingState()
    {
        IsRolling = true;
    }

    public void ResetRollingState()
    {
        IsRolling = false;
    }

    public int SetQuickAttackState(int quickAttackState)
    {
        return QuickAttackState = quickAttackState;
    }

    public void IsTouchedStairs(bool isOnStair)
    {
        IsOnStair = isOnStair;
    }

    public void ChangeStelsMode(bool stelsMode)
    {
        _isInStelsMode = stelsMode;
    }

    public void TakeDamage(int damage)
    {
        PlayerHp -= damage;
        Debug.Log("Took damage" + damage);
        OnTookDamage?.Invoke();
    }

    public void ChangeMeleeWeapon(WeaponsBase newMeleeWeapon)
    {
        CurrentMeleeWeapon = newMeleeWeapon;
    }

    public void ChangeLongRangeWeapon(WeaponsBase newLongRangeWeapon)
    {
        CurrentLongRangeWeapon = newLongRangeWeapon;
    }

    private void SetHaracteristicOnStart()
    {
        PlayerHp = _playerSettingsSO.Hp;
        CurrentMeleeWeapon = _playerSettingsSO.MeleeWeapon;
        CurrentLongRangeWeapon = _playerSettingsSO.LongRangeWeapon;
        CurrentAbility = _playerSettingsSO.Ability;
        CurrentArmor = _playerSettingsSO.PlayerArmor;
    }
}
