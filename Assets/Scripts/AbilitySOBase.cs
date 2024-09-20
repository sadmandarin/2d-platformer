using UnityEngine;

/// <summary>
/// ������� ����� ��� ������
/// </summary>
public abstract class AbilitySOBase : ScriptableObject
{
    public string AbilityName;
    public Sprite Icon;
    public string ShortDescription;
    public int Damage;
    public GameObject _abilityPrefab;
}
