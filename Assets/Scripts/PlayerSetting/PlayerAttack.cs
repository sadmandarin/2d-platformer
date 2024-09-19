using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
 
    private WeaponsBase _activeWeapon;
    private Player _player;

    private float _timeToStrongAttack = 1f;
    private float _lastTimeAttack;
    private float _magicAttackCooldown;
    private float _startAttackingTime;
    private bool _isAttacking;
    private int _valueOfQuickAttack = 0;

    private void Awake()
    {
        _lastTimeAttack = Time.time;
        _player = GetComponent<Player>();
        _activeWeapon = _player.CurrentMeleeWeapon;
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
                PerformAttack();
                _lastTimeAttack = Time.time;
            }
        }
    }

    void PerformAttack()
    {
        float duration = Time.time - _startAttackingTime;

        if (duration >= _timeToStrongAttack)
            _activeWeapon?.StrongAttack(_attackPoint, this);
        else
        {
            _activeWeapon?.QuickAttack(_attackPoint, this);
            _valueOfQuickAttack++;
            _player.SetQuickAttackState(_valueOfQuickAttack);
        }
            
    }

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

    public void SwitchActiveWeapon()
    {
        if (_activeWeapon == _player.CurrentMeleeWeapon)
        {
            _activeWeapon = _player.CurrentLongRangeWeapon ?? _activeWeapon;
        }
        else
        {
            _activeWeapon = _player.CurrentMeleeWeapon ?? _activeWeapon;
        }

        Debug.Log("Current weapon" + _activeWeapon);
    }
}
