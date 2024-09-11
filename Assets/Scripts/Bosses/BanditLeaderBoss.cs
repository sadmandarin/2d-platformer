using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BanditLeaderBoss : BossBase
{
    [SerializeField] private GameObject _specialAttackPrefab;
    [SerializeField] private Transform _spawnSpecialAttackTransform;

    private int _helmetArmorHealth;
    private int _bodyArmorHealth;
    private bool _isHelmetArmorDestroyed = false;
    private bool _isBodyArmorDestroyed = false;
    private int _hitToBlock = 4;
    private int _hitCounter = 0;
    private bool _canDoAttack = true;
    private bool _canDoSpecialAttack = true;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (Mathf.Abs(transform.position.x - _playerPosition.position.x) <= 2)
        {
            if (_canDoAttack)
            {
                _state = BossState.MeleeAttack;
            }
        }
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
        _animator.SetTrigger("Attack");

        Debug.Log("Attack");

        _canDoAttack = false;

        StartCoroutine(ResetAttack());

        switch (Random.Range(0, maxInclusive: 1))
        {
            case 0:
                _state = BossState.NoAction;
                break;
            case 1:
                _state = BossState.Retreat;
                break;
        }

        
    }

    public override void Block()
    {
        _isBlocking = true;
        Debug.Log("Блок был");
        StartCoroutine(EndBlock());
    }

    private IEnumerator EndBlock()
    {
        yield return new WaitForSeconds(1);

        _isBlocking = false;

        _state = BossState.NoAction;
    }

    public override void SpecialAttack()
    {
        if (_player != null)
        {
            Debug.Log("Специальная");

            GameObject specialAttack = Instantiate(_specialAttackPrefab, _spawnSpecialAttackTransform.position, Quaternion.identity);

            Vector2 direction = transform.localScale.x > 0 ? Vector2.left : Vector2.right;

            specialAttack.GetComponent<SpecialAttackProj>().Initialize(direction, _specialAttackDamage);

            _canDoSpecialAttack = false;

            StartCoroutine(ResetSpecialAttack());

            _state = BossState.NoAction;
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

        float specialAttack = Random.value;

        if (specialAttack <= 0.1 && _state != BossState.SpecialAttack)
        {
            if (_canDoSpecialAttack)
            {
                _state = BossState.SpecialAttack;
            }
        }
    }

    protected override void Retreat()
    {
        Debug.Log("Убегаем");

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
    }

    protected override IEnumerator ComboMeleeAttack()
    {
        for (int i = 0; i < Random.Range(2, 4); i++)
        {
            _animator.SetTrigger("Attack");
            Debug.Log("Комбо");

            yield return new WaitForSeconds(0.5f);
        }

        switch (Random.Range(0, 2))
        {
            case 0:
                _state = BossState.NoAction;
                break;
            case 1:
                _state = BossState.Retreat;
                break;
        }
    }

    public override void SetPlayer(Player player)
    {
        if (_player == null)
        {
            _player = player;

            _playerPosition = player.gameObject.transform;

            _state = BossState.SpecialAttack;
        }
    }

    private IEnumerator ResetSpecialAttack()
    {
        yield return new WaitForSeconds(2);

        _canDoSpecialAttack = true;
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1);

        _canDoAttack = true;
    }

    protected override IEnumerator AttackPattern()
    {
        throw new System.NotImplementedException();
    }
}
