using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _playerHp;

    [SerializeField] private bool _isOnGround = true;
    private bool _isOnStair;
    private bool _isInStelsMode;
    private float _isFalling;
    [SerializeField] private bool _isJumping;
    private bool _isTouchingWall;
    [SerializeField] private int _isMoving;
    private int _quickAttackState = 0;

    private Animator _animator;

    public int MaxHP = 100;
    public event Action OnTookDamage;


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
}
