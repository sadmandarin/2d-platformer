using UnityEngine;

/// <summary>
/// ������� ����� ��� ������
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
    /// ������� �����
    /// </summary>
    /// <param name="attackpoint">����� �����</param>
    /// <param name="owner"></param>
    public abstract void QuickAttack(Transform attackpoint, MonoBehaviour owner);

    /// <summary>
    /// ������� �����
    /// </summary>
    /// <param name="attackPoint">����� �����</param>
    /// <param name="owner"></param>
    public abstract void StrongAttack(Transform attackPoint, MonoBehaviour owner);

}
