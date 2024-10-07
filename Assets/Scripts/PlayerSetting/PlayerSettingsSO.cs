using UnityEngine;

/// <summary>
/// SO � ���������������� ������ ��� ���������� � ������ ������
/// </summary>
[CreateAssetMenu(fileName = "PlayerSettingsSO", menuName = "ScriptableObjects/PlayerSettings", order = 1)]
public class PlayerSettingsSO : ScriptableObject
{
    public int Hp;
    public WeaponsBase MeleeWeapon;
    public GameObject LongRangeWeapon;
    public AbilitySOBase Ability;
    public PlayerArmorBase PlayerArmor;

    /// <summary>
    /// ��������� �������� ����������� ������
    /// </summary>
    /// <param name="ability"></param>
    public void SetHaracterictic(AbilitySOBase ability)
    {
        Ability = ability;
    }

    /// <summary>
    /// ��������� �������� ����������
    /// </summary>
    /// <param name="weapon"></param>
    public void SetHaracterictic(WeaponsBase weapon)
    {
        //if (weapon.IsRanged)
        //{
        //    LongRangeWeapon = weapon;
        //}
        MeleeWeapon = weapon;
        
    }

    /// <summary>
    /// ��������� ������� �����
    /// </summary>
    /// <param name="playerArmor"></param>
    public void SetHaracterictic(PlayerArmorBase playerArmor)
    {
        PlayerArmor = playerArmor;
    }
}
