using System.Collections;
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
    private Player _player;

    protected override void Start()
    {
        base.Start();
    }

    public void SetPlayer(Player player)
    {
        _player = player;
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

    }

    public override void Block()
    {
        _isBlocking = true;
        Debug.Log("Ѕлок был");
        StartCoroutine(EndBlock());
    }

    private IEnumerator EndBlock()
    {
        yield return new WaitForSeconds(1);
        _isBlocking = false;
    }

    public override void SpecialAttack()
    {
        GameObject specialAttack = Instantiate(_specialAttackPrefab, _spawnSpecialAttackTransform.position, Quaternion.identity);

        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        specialAttack.GetComponent<SpecialAttackProj>().Initialize(direction, _specialAttackDamage);
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
            _currenHp -= damage;

            if (_currenHp <= 0)
            {
                Die();
            }
        }
        else
            ArmorDamage(damage, isRanged);
    }

    protected override void Die()
    {
        _isDead = true;
        _animator.SetTrigger("Death");
        StartCoroutine(DestroyBossAfterDeath());
    }

    private IEnumerator DestroyBossAfterDeath()
    {
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
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

    private IEnumerator AttackPattern()
    {
        yield return null;
    }

    void MoveTowardsPlayer()
    {

    }
}
