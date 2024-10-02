using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// второй босс
/// </summary>
public class OrkBoss : BossBase
{
    private bool _doAction = true;
    private bool _canDoSpecialAttack = true;
    private bool _canDoAttack = true;
    private float _attackRange = 1.7f;

    private int _comboSteps = 0;
    private int _comboCount;
    private float _comboTimer = 0;
    private float _comboDelay = 1.5f;
    private bool _isComboActive = false;

    private Vector2 _destinationForSpecialAttack = Vector2.zero;

    private List<BossState> _avaiableState;
    private bool _isBodyArmorDestroyed = false;

    private bool _canThrowTrap = true;
    private int _trapSpawnDelay = 11;
    public int _currentTrapCount = 0;
    private int _maxTrapCount = 2;
    [SerializeField] private GameObject _trapPrefab;

    protected override void Awake()
    {
        base.Awake();

        Array values = Enum.GetValues(typeof(BossState));

        _avaiableState = new List<BossState>((BossState[])values);
        _avaiableState.Remove(BossState.Retreat);
        _avaiableState.Remove(BossState.Idle);
    }

    public override void Attack()
    {
        if (Vector2.Distance(_playerPosition.position, transform.position) >= 0.5f)
        {
            MoveTowardsPlayer(6, _player.transform.position);
        }

        if (Mathf.Abs(transform.position.x - _playerPosition.position.x) <= 1)
        {
            if (_canDoAttack)
            {
                _animator.SetTrigger("Attack");

                Debug.Log("Attack");

                _canDoAttack = false;

                _state = BossState.Idle;

                _doAction = true;

                StartCoroutine(ResetAttack());
            }
        }
    }

    public void AttackOnAnimationEvent()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPointTransform.position, _attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Player>())
            {
                if (!_player.IsRolling)
                {
                    _player.TakeDamage(_baseDamage);
                }
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
        yield return new WaitForSeconds(1);

        _isBlocking = false;
    }

    public override void SetPlayer(Player player)
    {
        if (_player == null)
        {
            _player = player;

            _playerPosition = player.gameObject.transform;

            StartCoroutine(AttackInterval());
        }
    }

    public override void SpecialAttack()
    {
        _doAction = false;

        if (_canDoSpecialAttack)
        {
            _canDoSpecialAttack = false;

            _destinationForSpecialAttack = (_player.transform.position.x > transform.position.x) ?
                (new Vector2((_player.transform.position.x + 2), transform.position.y)) :
                (new Vector2((_player.transform.position.x - 2), transform.position.y));
        }

        if (_destinationForSpecialAttack != Vector2.zero)
        {
            MoveTowardsPlayer(10, _destinationForSpecialAttack);
        }
        
        if (Vector2.Distance(transform.position, _destinationForSpecialAttack) <= 0.1)
        {
            _state = BossState.Idle;

            _destinationForSpecialAttack = Vector2.zero;

            StartCoroutine(ResetSpecialAttack());
        }
    }


    protected override IEnumerator AttackInterval()
    {
        while (true)
        {
            if (_doAction)
            {
                var random = UnityEngine.Random.Range(0, _avaiableState.Count);

                _state = _avaiableState[random];

                Debug.Log(_state);
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));
        }
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

    protected override void IdleMove()
    {
        if (Vector2.Distance(_playerPosition.position, transform.position) >= 4f)
        {
            MoveTowardsPlayer(2.5f, _player.transform.position);
        }
    }

    protected override void InitializeStats()
    {
        _baseDamage = 25;
        _baseHealth = 300;
        _baseMaxHealth = 300;
        _baseSpeed = 4f;
        _helmetArmorHealth = 0;
        _bodyArmorHealth = _baseHealth;
        _allArmorHealth = _helmetArmorHealth + _bodyArmorHealth;
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

    public override void ArmorDamage(int damage, bool isRanged)
    {
        var armorDamageMuptiplier = isRanged ? 1.2f : 1f;

        if (!_isBodyArmorDestroyed)
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

    public override void TakeDamage(int damage, bool isRanged)
    {

        base.TakeDamage(damage, isRanged);
    }

    protected override void Retreat()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Метод, спавнящий ловушку на поле
    /// </summary>
    private void ThrowTraps()
    {
        if (_canThrowTrap && _currentTrapCount < _maxTrapCount)
        {
            Vector2 forwardHitPosition;
            Vector2 backwardHitPosition;

            RaycastHit2D forwardHit = Physics2D.Raycast(transform.position + Vector3.up, Vector2.right, 5f);
            RaycastHit2D backwardHit = Physics2D.Raycast(transform.position + Vector3.up, Vector2.left, 5f);

            if (forwardHit.collider != null && forwardHit.collider.gameObject.GetComponent<Ground>())
                forwardHitPosition = forwardHit.point;
            else
                forwardHitPosition = (Vector2)transform.position + Vector2.right * 5;

            if (backwardHit.collider != null && backwardHit.collider.gameObject.GetComponent<Ground>())
                backwardHitPosition = backwardHit.point;
            else
                backwardHitPosition = (Vector2)transform.position + Vector2.left * 5;

            Vector2 spawnPos = new(UnityEngine.Random.Range(forwardHitPosition.x, backwardHitPosition.x), transform.position.y);

            GameObject trap = Instantiate(_trapPrefab, spawnPos, Quaternion.identity);

            _currentTrapCount++;
            _canThrowTrap = false;

            var trapScript = trap.GetComponent<OrkBossTrap>();
            trapScript.SetParent(this);

            StartCoroutine(ResetThrowingTraps());

            _state = BossState.Idle;
        }
        else
            _state = BossState.Idle;
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

    private IEnumerator ResetThrowingTraps()
    {
        yield return new WaitForSeconds(_trapSpawnDelay);

        _canThrowTrap = true;
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
            case BossState.TrapDeploy:
                ThrowTraps();
                break;
        }
    }
}
