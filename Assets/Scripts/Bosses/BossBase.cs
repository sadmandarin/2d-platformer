using UnityEngine;

[RequireComponent (typeof(Animator), typeof(Rigidbody2D))]
public abstract class BossBase : MonoBehaviour
{
    protected int _baseDamage;
    protected int _specialAttackDamage;
    protected int _baseHealth;
    protected float _baseSpeed;
    protected float _reduceDamageByBlock = 0.7f;


    protected int _currenHp;
    protected bool _isBlocking;
    protected bool _isPlayerDetected;
    protected bool _isDead = false;

    protected Animator _animator;

    protected virtual void Start()
    {
        _animator = GetComponent<Animator> ();

        InitializeStats();
    }

    protected abstract void InitializeStats();

    public abstract void TakeDamage(int damage, bool isRanged);
    protected abstract void Die();
    public abstract void Attack();
    public abstract void Block();
    public abstract void SpecialAttack();
    public abstract void ArmorDamage(int damage, bool isRanged);
}
