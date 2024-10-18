using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Класс атаки игрока
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
 
    private WeaponsBase _activeWeapon;
    private Player _player;
    private Inputs _inputs;

    private float _timeToStrongAttack = 1f;
    private float _lastTimeAttack;
    private float _startAttackingTime;
    private int _valueOfQuickAttack = 0;
    private bool _isAttacking;
    private float _startMagicAttackTime;

    public AbilitySOBase _activeAbility;

    private void Awake()
    {
        _lastTimeAttack = Time.time;

        _player = GetComponent<Player>();

        _activeWeapon = _player.CurrentMeleeWeapon;

        _inputs = new Inputs();
        
    }

    private void OnEnable()
    {
        _inputs.GamePlay.Attack.performed +=OnAttackPerformed;
        _inputs.GamePlay.Attack.canceled += OnAttackCanceled;

        _inputs.GamePlay.MagicAbilty.performed += MagicAttack;

        _inputs.GamePlay.ThrowWeapon.performed += ThrowingWeapons;

        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.GamePlay.Attack.performed -= OnAttackPerformed;
        _inputs.GamePlay.Attack.canceled -= OnAttackCanceled;

        _inputs.GamePlay.MagicAbilty.performed -= MagicAttack;

        _inputs.GamePlay.ThrowWeapon.performed -= ThrowingWeapons;

        _inputs.Disable();
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if (!_player.IsStunned)
        {
            if (Time.time >= _lastTimeAttack + 1 / _activeWeapon.AttackSpeed)
            {
                if (!_player.IsRolling)
                {
                    _startAttackingTime = Time.time;
                }
            }
        }
    }

    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        if (!_player.IsStunned)
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
    }

    /// <summary>
    /// Метательное оружие
    /// </summary>
    private void ThrowingWeapons(InputAction.CallbackContext context)
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
    public void MagicAttack(InputAction.CallbackContext context)
    {
        if (_player.CurrentAbility != null)
        {
            if (!_player.IsStunned)
            {
                if (Time.time >= _startMagicAttackTime + _activeAbility.Delay)
                {
                    if (!_player.IsRolling)
                    {
                        Debug.Log("Magic");
                        if (_player.CurrentAbility != null)
                        {
                            var magicAbility = Instantiate(_player.CurrentAbility, _attackPoint.position, Quaternion.identity);

                            Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
                            magicAbility.GetComponent<AbilitySOBase>().InitializeStats(direction, _attackPoint.position);
                            _player.DecreaseMana(_activeAbility.ManaCost);
                            //после того, как обсудим магические способности
                        }

                        _startMagicAttackTime = Time.time;
                    }
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.position, 1f);
    }
}
