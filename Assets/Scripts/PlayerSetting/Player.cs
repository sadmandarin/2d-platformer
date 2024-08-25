using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _playerHp;

    [SerializeField] private bool _isOnGround = true;
    private bool _isOnStair;
    private bool _isInStelsMode;
    [SerializeField] private bool _isTouchingWall;

    public int MaxHP = 100;
    public event Action OnTookDamage;

    public float PlayerHp { get { return _playerHp; } private set { _playerHp = value; } }
    public bool IsOnGround {  get { return _isOnGround; } private set {  _isOnGround = value; } }
    public bool IsOnStair { get {  return _isOnStair; } private set { _isOnStair = value; } }
    public bool IsInStelsMode { get {  return _isInStelsMode; } private set { _isInStelsMode = value; } }

    public bool IsTouchingWall { get { return _isTouchingWall; } private set { _isTouchingWall = value; } }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            TakeDamage(5);
        }
    }

    public void GroundChecker(bool isOnGround)
    {
        _isOnGround = isOnGround;
    }

    public void TouchedWall(bool touch)
    {
        IsTouchingWall = touch;
    }

    public void IsTouchedStairs(bool isOnStair)
    {
        _isOnStair = isOnStair;
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
