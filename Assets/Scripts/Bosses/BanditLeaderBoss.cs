using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ����
/// </summary>
public class BanditLeaderBoss : BossBase
{
    [SerializeField] private GameObject _specialAttackPrefab;
    [SerializeField] private Transform _spawnSpecialAttackTransform;

    private int _helmetArmorHealth;
    private int _bodyArmorHealth;
    private int _comboSteps = 0;
    private int _comboCount;
    private float _comboTimer = 0;
    private float _comboDelay = 0.5f;
    private bool _isComboActive = false;
    private bool _isHelmetArmorDestroyed = false;
    private bool _isBodyArmorDestroyed = false;
    private int _hitToBlock = 4;
    private int _hitCounter = 0;
    private bool _canDoAttack = true;
    private bool _canDoSpecialAttack = true;
    private bool _doAction = true;

    private List<BossState> _avaiableState;

    protected override void Start()
    {
        base.Start();

        Array values = Enum.GetValues(typeof(BossState));

        _avaiableState = new List<BossState>((BossState[])values);
        _avaiableState.Remove(BossState.Block);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void ArmorDamage(int damage, bool isRanged)
    {
        float armorDamageMuptiplier = isRanged ? 1.2f : 1f;

        if (!_isHelmetArmorDestroyed)
        {
            _helmetArmorHealth -= (int)(damage * armorDamageMuptiplier);

            if (_helmetArmorHealth <= 0)
                _isHelmetArmorDestroyed = true;
            
        }

        else if (!_isBodyArmorDestroyed)
        {
            _bodyArmorHealth = (int)(damage * armorDamageMuptiplier);

            if (_bodyArmorHealth <= 0)
                _isBodyArmorDestroyed = true;
        }
    }

    public override void Attack()
    {
        if (Vector2.Distance(_playerPosition.position, transform.position) >= 0.5f)
        {
            MoveTowardsPlayer();
        }

        if (Mathf.Abs(transform.position.x - _playerPosition.position.x) <= 1)
        {
            if (_canDoAttack)
            {
                _animator.SetTrigger("Attack");
                //������� �������� �� IsRolling �� ����� ������ ���� �����
                Debug.Log("Attack");

                _canDoAttack = false;

                _state = BossState.Idle;

                _doAction = true;

                StartCoroutine(ResetAttack());
            }
        }
    }

    public override void Block()
    {
        _isBlocking = true;
        Debug.Log("���� ���");
        StartCoroutine(EndBlock());
    }

    /// <summary>
    /// ��������� ��������� �����
    /// </summary>
    /// <returns></returns>
    private IEnumerator EndBlock()
    {
        yield return new WaitForSeconds(1);

        _isBlocking = false;
    }

    public override void SpecialAttack()
    {
        if (_canDoSpecialAttack)
        {
            Debug.Log("�����������");

            GameObject specialAttack = Instantiate(_specialAttackPrefab, _spawnSpecialAttackTransform.position, Quaternion.identity);

            Vector2 direction = transform.localScale.x > 0 ? Vector2.left : Vector2.right;

            specialAttack.GetComponent<SpecialAttackProj>().Initialize(direction, _specialAttackDamage);

            _canDoSpecialAttack = false;

            _state = BossState.Idle;

            _doAction = true;

            StartCoroutine(ResetSpecialAttack());
        }
    }

    public override void TakeDamage(int damage, bool isRanged)
    {
        if (_hitCounter % _hitToBlock == 0)
        {
            _state = BossState.Block;
        }

        if (_isBlocking)
        {
            damage = (int)(damage * 0.7f);
        }

        if (_isHelmetArmorDestroyed && _isBodyArmorDestroyed)
        {
            _currenHp -= damage;

            if (_currenHp <= 0)
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
        _baseHealth = 800;
        _baseSpeed = 4.4f;

        _currenHp = _baseHealth;
        _helmetArmorHealth = 400;
        _bodyArmorHealth = _baseHealth;
    }

    protected override void Idle()
    {
        if (Vector2.Distance(_playerPosition.position, transform.position) >= 4f)
        {
            MoveTowardsPlayer();
        }
    }

    protected override void MoveTowardsPlayer()
    {
        Vector2 direction = (_playerPosition.position - transform.position).normalized;

        _rb.velocity = new Vector2(direction.x * _baseSpeed, _rb.velocity.y);
        
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-2, 2, 2); 
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(2, 2, 2);
        }

        float specialAttack = UnityEngine.Random.value;

        if (specialAttack <= 0.1 && _state != BossState.SpecialAttack)
        {
            if (_canDoSpecialAttack)
            {
                _state = BossState.SpecialAttack;
            }
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

                _doAction = false;

                Debug.Log(_state);
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 2f));
        }
    }

    protected override void Retreat()
    {
        Debug.Log("�������");

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
        if (!_isComboActive)
        {
            _animator.SetTrigger("Attack");

            _isComboActive = true;

            _comboSteps++;

            _comboCount = UnityEngine.Random.Range(0, 4);
        }


        if (Vector2.Distance(_playerPosition.position, transform.position) >= 1f)
        {
            MoveTowardsPlayer();
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

            StartCoroutine(AttackInterval());
        }
    }

    /// <summary>
    /// ����� ��������� ����������� ����� ����� ��������� ��������� ��������
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// ����� �������� � 2 ������� ���������� �����, �������� ������ ��� NPC ����� ��������� �������� � ������������ ����������� �����.
    /// </remarks>
    private IEnumerator ResetSpecialAttack()
    {
        yield return new WaitForSeconds(2);

        _doAction = true;

        _canDoSpecialAttack = true;
    }

    /// <summary>
    /// ����� ��������� ����� ����� ��������� ��������� ��������
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// ����� �������� � 2 ������� ���������� �����, �������� ������ ��� NPC ����� ��������� �������� � ������������ ����������� �����.
    /// </remarks>
    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f);

        _doAction = true;

        _canDoAttack = true;
    }
}
