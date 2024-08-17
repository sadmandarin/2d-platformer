using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _hp = 100;
    private float _damage;
    [SerializeField] private bool _isOnGround;
    private bool _isObstacleAhead;
    private bool _isOnStairs;
    private bool _isPlayerDetected;
    private Transform _playerTransform;

    public float Hp { get { return _hp; } private set { _hp = value; } }
    public float Damage { get { return _damage; } private set { _damage = value; } }
    public bool IsOnGround { get { return _isOnGround; } private set { _isOnGround = value; } }
    public bool IsObstacleAhead { get { return _isObstacleAhead; } private set { _isObstacleAhead = value; } }
    public bool IsOnStairs { get {  return _isOnStairs; } private set { _isOnStairs = value; } }
    public bool IsPlayerDetected { get { return _isPlayerDetected; } private set { _isPlayerDetected = value; } }
    public Transform PlayerTransform { get { return _playerTransform; } private set { _playerTransform = value; } }

    public void SetPlayerDetect(bool detect)
    {
        IsPlayerDetected = detect;
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        PlayerTransform = playerTransform;
    }

    public void IsTouchedStairs(bool isTouchedStairs)
    {
        IsOnStairs = isTouchedStairs;
    }

    public void GroundChecker(bool touchedGround)
    {
        IsOnGround = touchedGround;
    }

    internal void TakeDamage(int damage)
    {
        Hp -= damage;

        Debug.Log($"Получено урона врагом: {damage}");
        Debug.Log($"Осталось HP у врага: {Hp}");

        if (Hp <= 0)
            Destroy(gameObject);
    }
}
