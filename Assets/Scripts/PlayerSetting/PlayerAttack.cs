using System;
using UnityEngine;

/// <summary>
/// Класс атаки игрока
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
 
    private WeaponsBase _activeWeapon;
    private Player _player;

    private float _timeToStrongAttack = 1f;
    private float _lastTimeAttack;
    private float _magicAttackCooldown;
    private float _startAttackingTime;
    private int _valueOfQuickAttack = 0;

    private void Awake()
    {
        _lastTimeAttack = Time.time;
        _player = GetComponent<Player>();
        _activeWeapon = _player.CurrentMeleeWeapon;
    }

    private void Update()
    {
        if (!_player.IsStunned)
        {
            //if (Input.GetKeyDown(KeyCode.Q))
            //{
            //    SwitchActiveWeapon();
            //}

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (Time.time >= _lastTimeAttack + 1 / _activeWeapon.AttackSpeed)
                {
                    if (!_player.IsRolling)
                    {
                        _startAttackingTime = Time.time;
                    }
                }
            }

            else if (Input.GetKeyUp(KeyCode.F))
            {
                if (Time.time >= _lastTimeAttack + 1 / _activeWeapon.AttackSpeed)
                {
                    if (!_player.IsRolling)
                    {
                        _lastTimeAttack = Time.time;
                        PerformAttack();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                ThrowingWeapons();
            }
        }
    }

    private void ThrowingWeapons()
    {
        var throwWeapon = Instantiate(_player.CurrentLongRangeWeapon, _attackPoint.position, Quaternion.identity);

        var throwWeaponScript = throwWeapon.GetComponent<ThrowingWeaponBase>();
        StartCoroutine(throwWeaponScript.ThrowingTrajectory((int)transform.localScale.x, _attackPoint.position));
    }

    /// <summary>
    /// Атака
    /// </summary>
    void PerformAttack()
    {
        float duration = _lastTimeAttack - _startAttackingTime;

        if (duration >= _timeToStrongAttack)
        {
            _activeWeapon?.StrongAttack(_attackPoint, this);
            _player.SetStrongAttack();
        }
            
        else
        {
            _activeWeapon?.QuickAttack(_attackPoint, this);
            _valueOfQuickAttack++;
            _player.SetQuickAttackState(_valueOfQuickAttack);
        }
            
    }

    /// <summary>
    /// Магическая атака
    /// </summary>
    public void MagicAttack()
    {
        if (_player.CurrentAbility != null)
        {
            //после того, как обсудим магические способности
        }
    }

    void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.position, 1f);
    }

    /// <summary>
    /// Переключение типов оружия
    /// </summary>
    //public void SwitchActiveWeapon()
    //{
    //    //if (_activeWeapon == _player.CurrentMeleeWeapon)
    //    //{
    //    //    _activeWeapon = _player.CurrentLongRangeWeapon ?? _activeWeapon;
    //    //}
        
    //    {
    //        _activeWeapon = _player.CurrentMeleeWeapon ?? _activeWeapon;
    //    }

    //    Debug.Log("Current weapon" + _activeWeapon);
    //}
}
