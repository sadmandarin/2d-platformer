using UnityEngine;

public abstract class WeaponsBase : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public string ShortDescription;
    public float AttackSpeed;
    public int QuickAttackDamage;
    public int StrongAttackDamage;
    public bool IsRanged;

    public abstract void QuickAttack(Transform attackpoint, MonoBehaviour owner);
    public abstract void StrongAttack(Transform attackPoint, MonoBehaviour owner);

}
