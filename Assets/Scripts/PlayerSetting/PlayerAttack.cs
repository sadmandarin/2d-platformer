using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private WeaponsBase _currentMeleeWeapon;
    [SerializeField] private WeaponsBase _currentLongRangeWeapon;

    private WeaponsBase _activeWeapon;

    private float _timeToStrongAttack = 1f;
    private float _lastTimeAttack;

    private float _startAttackingTime;
    private bool _isAttacking;

    private void Start()
    {
        _lastTimeAttack = Time.time;
        _activeWeapon = _currentMeleeWeapon;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchActiveWeapon();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Time.time >= _lastTimeAttack + 1/_activeWeapon.AttackSpeed)
            {
                _startAttackingTime = Time.time;
                _isAttacking = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (_isAttacking)
            {
                PerformAttack();
                _isAttacking = false;
            }
        }
    }

    void PerformAttack()
    {
        float duration = Time.time - _startAttackingTime;

        if (duration >= _timeToStrongAttack)
            _activeWeapon?.TryAttack(_activeWeapon.StrongAttackDamage);
        else
            _activeWeapon?.TryAttack(_activeWeapon.QuickAttackDamage);
    }

    public void SwitchActiveWeapon()
    {
        if (_activeWeapon == _currentMeleeWeapon)
        {
            _activeWeapon = _currentLongRangeWeapon ?? _activeWeapon;
        }
        else
        {
            _activeWeapon = _currentMeleeWeapon ?? _activeWeapon;
        }

        Debug.Log("Current weapon" + _activeWeapon);
    }

    public void ChangeMeleeWeapon(WeaponsBase newMeleeWeapon)
    {
        if (_activeWeapon == _currentMeleeWeapon)
        {
            _activeWeapon = newMeleeWeapon;
        }
        _currentMeleeWeapon = newMeleeWeapon;     
    }

    public void ChangeLongRangeWeapon(WeaponsBase newLongRangeWeapon)
    {
        if (_activeWeapon == _currentLongRangeWeapon)
        {
            _activeWeapon = newLongRangeWeapon;
        }
        _currentLongRangeWeapon = newLongRangeWeapon;
    }
}
