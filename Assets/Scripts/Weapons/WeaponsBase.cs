using UnityEngine;

public abstract class WeaponsBase : ScriptableObject
{
    public string Name;
    public float AttackSpeed;
    public int QuickAttackDamage;
    public int StrongAttackDamage;

    public void TryAttack(int damage)
    {
        Attack(damage);
    }

    protected abstract void Attack(int damage);
}
