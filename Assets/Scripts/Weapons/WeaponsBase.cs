using UnityEngine;

public abstract class WeaponsBase : ScriptableObject
{
    public string Name;
    public float AttackSpeed;
    public int QuickAttackDamage;
    public int StrongAttackDamage;

    public abstract void QuickAttack(Transform attackpoint, MonoBehaviour owner);
    public abstract void StrongAttack(Transform attackPoint, MonoBehaviour owner);

}
