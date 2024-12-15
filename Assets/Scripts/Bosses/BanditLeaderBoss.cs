using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Первый босс
/// </summary>
public class BanditLeaderBoss : BossBase
{
    [SerializeField] private GameObject _specialAttackPrefab;
    [SerializeField] private Transform _SpecialAttackPointTransform;

    
    private int _comboSteps = 0;
    private int _comboCount;
    private float _comboTimer = 0;
    private float _comboDelay = 1f;
    private bool _isComboActive = false;
    private float _attackRange = 1.7f;
    private bool _isHelmetArmorDestroyed = false;
    private bool _isBodyArmorDestroyed = false;
    private int _hitToBlock = 4;
    private int _hitCounter = 0;
    private bool _canDoAttack = true;
    private bool _canDoSpecialAttack = true;
    private bool _doAction = true;

    private List<BossState> _avaiableState;

    protected override void Awake()
    {
        base.Awake();

        Array values = Enum.GetValues(typeof(BossState));

        _avaiableState = new List<BossState>((BossState[])values);
        _avaiableState.Remove(BossState.Idle);
        _avaiableState.Remove(BossState.TrapDeploy);
    }

    public override void ArmorDamage(int damage, bool isRanged)
    {
        float armorDamageMuptiplier = isRanged ? 1.2f : 1f;

        if (!_isHelmetArmorDestroyed)
        {
            _helmetArmorHealth -= (int)(damage * armorDamageMuptiplier);

            if (_helmetArmorHealth <= 0)
            {
                _helmetArmorHealth = 0;

                _isHelmetArmorDestroyed = true;
            }
            
        }
        else if (!_isBodyArmorDestroyed)
        {
            _bodyArmorHealth -= (int)(damage * armorDamageMuptiplier);

            if (_bodyArmorHealth <= 0)
            {
                _bodyArmorHealth = 0;

                _isBodyArmorDestroyed = true;
            }
        }

        base.ArmorDamage(damage, isRanged);
    }

    public override void Attack()
    {
        if (Vector2.Distance(_playerPosition.position, transform.position) >= 0.5f)
        {
            MoveTowardsPlayer(7, _player.transform.position);
        }

        if (Mathf.Abs(transform.position.x - _playerPosition.position.x) <= 1)
        {
            if (_canDoAttack)
            {
                _animator.SetTrigger("Attack");

                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPointTransform.position, _attackRange);

                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.GetComponent<Player>())
                    {
                        if (!_player.IsRolling)
                        {
                            _player.TakeDamage(_baseDamage, transform);
                        }
                    }
                }
                Debug.Log("Attack");

                _canDoAttack = false;

                _state = BossState.Idle;

                _doAction = false; 

                StartCoroutine(ResetAttack());
            }
        }
    }

    public override void Block()
    {
        if (_isBlocking == false)
        {
            _isBlocking = true;

            StartCoroutine(EndBlock());
        }
    }

    /// <summary>
    /// Окончание состояния блока
    /// </summary>
    /// <returns></returns>
    private IEnumerator EndBlock()
    {
        yield return new WaitForSeconds(0.5f);

        _isBlocking = false;
    }

    public override void SpecialAttack()
    {
        if (_canDoSpecialAttack)
        {
            Debug.Log("Специальная");

            GameObject specialAttack = Instantiate(_specialAttackPrefab, _SpecialAttackPointTransform.position, Quaternion.identity);

            Vector2 direction = transform.localScale.x > 0 ? Vector2.left : Vector2.right;

            specialAttack.GetComponent<EnemyLongRangeAttackArrow>().Initialize(direction, _specialAttackDamage);

            _canDoSpecialAttack = false;

            _state = BossState.Idle;

            _doAction = false;

            StartCoroutine(ResetSpecialAttack());
        }
    }

    public override void TakeDamage(int damage, bool isRanged)
    {
        if (_hitCounter % _hitToBlock == 0)
        {
            Block();
        }

        if (_isBlocking)
        {
            damage = (int)(damage * 0.7f);
        }

        if (_isHelmetArmorDestroyed && _isBodyArmorDestroyed)
        {
            _baseHealth -= damage;

            base.TakeDamage(damage, isRanged);

            if (_baseHealth <= 0)
            {
                Die();
            }
        }
        else
            ArmorDamage(damage, isRanged);
    }

    protected override void InitializeStats()
    {
        _baseDamage = 30;
        _baseHealth = 600;
        _baseMaxHealth = 600;
        _baseSpeed = 4.4f;
        _helmetArmorHealth = 400;
        _bodyArmorHealth = _baseHealth;
        _allArmorHealth = _helmetArmorHealth + _bodyArmorHealth;
    }

    protected override void IdleMove()
    {
        if (Vector2.Distance(_playerPosition.position, transform.position) >= 4f)
        {
            MoveTowardsPlayer(2.5f, _player.transform.position);   
        }
    }

    protected override void MoveTowardsPlayer(float speed, Vector2 destination)
    {
        Vector2 direction = (destination - (Vector2)transform.position).normalized;

        _rb.velocity = new Vector2(direction.x * speed, _rb.velocity.y);
        
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-2, 2, 2); 
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(2, 2, 2);
        }

        //float specialAttack = UnityEngine.Random.value;

        //if (specialAttack <= 0.1 && _state != BossState.SpecialAttack)
        //{
        //    if (_canDoSpecialAttack)
        //    {
        //        _state = BossState.SpecialAttack;
        //    }
        //}
    }

    protected override IEnumerator AttackInterval()
    {
        while (true)
        {
            if (_doAction)
            {
                var random = UnityEngine.Random.Range(0, _avaiableState.Count);

                _state = _avaiableState[random];

                _doAction = false;

                Debug.Log(_state);
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 2f));
        }
    }

    protected override void Retreat()
    {
        Debug.Log("Убегаем");

        _doAction = false;

        Vector2 direction = (_playerPosition.position - transform.position).normalized;

        _rb.velocity = new Vector2(-direction.x * _baseSpeed, _rb.velocity.y);

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(2, 2, 2);
        }

        if (Vector2.Distance(transform.position, _playerPosition.position) > 5)
        {
            _state = BossState.SpecialAttack;
        }

        _doAction = true;
    }

    protected override void ComboMeleeAttack()
    {
        _doAction = false;

        if (!_isComboActive)
        {
            _animator.SetTrigger("Attack");

            _isComboActive = true;

            _comboSteps++;

            _comboCount = UnityEngine.Random.Range(0, 4);
        }


        if (Vector2.Distance(_playerPosition.position, transform.position) >= 1f)
        {
            MoveTowardsPlayer(7, _player.transform.position);
        }

        _comboTimer += Time.deltaTime;



        if (Vector2.Distance(_playerPosition.position, transform.position) <= 2f)
        {
            if (_comboTimer >= _comboDelay)
            {
                if (_comboSteps < _comboCount)
                {
                    _comboSteps++;
                    _comboTimer = 0;
                    _animator.SetTrigger("Attack");
                }
                else
                {
                    _isComboActive = false;
                    _comboCount = 0;
                    _comboSteps = 0;
                }
            }
        }
        if (!_isComboActive)
        {
            _doAction = true;

            _state = BossState.Idle;
        }
    }

    public override void SetPlayer(Player player)
    {
        if (_player == null)
        {
            _player = player;

            _playerPosition = player.gameObject.transform;

            OnTakeBestiary();

            StartCoroutine(AttackInterval());
        }
    }

    /// <summary>
    /// Сброс состояния специальной атаки после заданного интервала ожидания
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// После задержки в 2 секунды сбрасывает флаги, позволяя игроку или NPC снова выполнять действие и использовать специальную атаку.
    /// </remarks>
    private IEnumerator ResetSpecialAttack()
    {
        yield return new WaitForSeconds(2);

        _doAction = true;

        _canDoSpecialAttack = true;
    }

    /// <summary>
    /// Сброс состояния атаки после заданного интервала ожидания
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// После задержки в 2 секунды сбрасывает флаги, позволяя игроку или NPC снова выполнять действие и использовать специальную атаку.
    /// </remarks>
    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f);

        _doAction = true;

        _canDoAttack = true;
    }

    protected override void HandleState()
    {
        switch (_state)
        {
            case BossState.Idle:
                IdleMove();
                break;
            case BossState.MeleeAttack:
                Attack();
                break;
            case BossState.ComboMeleeAttack:
                ComboMeleeAttack();
                break;
            case BossState.SpecialAttack:
                SpecialAttack();
                break;
            case BossState.Retreat:
                Retreat();
                break;
        }
    }
}
