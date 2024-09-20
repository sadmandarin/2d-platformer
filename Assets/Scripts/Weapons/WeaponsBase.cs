using UnityEngine;

/// <summary>
/// Базовый класс для оружия
/// </summary>
public abstract class WeaponsBase : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public string ShortDescription;
    public float AttackSpeed;
    public int QuickAttackDamage;
    public int StrongAttackDamage;
    public bool IsRanged;

    /// <summary>
    /// Быстрая атака
    /// </summary>
    /// <param name="attackpoint">Место атаки</param>
    /// <param name="owner"></param>
    public abstract void QuickAttack(Transform attackpoint, MonoBehaviour owner);

    /// <summary>
    /// Сильная атака
    /// </summary>
    /// <param name="attackPoint">Точка атаки</param>
    /// <param name="owner"></param>
    public abstract void StrongAttack(Transform attackPoint, MonoBehaviour owner);

}
