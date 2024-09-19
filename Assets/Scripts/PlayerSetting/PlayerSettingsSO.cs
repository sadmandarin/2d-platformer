using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettingsSO", menuName = "ScriptableObjects/PlayerSettings", order = 1)]
public class PlayerSettingsSO : ScriptableObject
{
    public int Hp;
    public WeaponsBase MeleeWeapon;
    public WeaponsBase LongRangeWeapon;
    public AbilitySOBase Ability;
    public PlayerArmorBase PlayerArmor;

    public void SetHaracterictic(AbilitySOBase ability)
    {
        Ability = ability;
    }

    public void SetHaracterictic(WeaponsBase weapon)
    {
        if (weapon.IsRanged)
        {
            LongRangeWeapon = weapon;
        }
        else
        {
            MeleeWeapon = weapon;
        }
    }

    public void SetHaracterictic(PlayerArmorBase playerArmor)
    {
        PlayerArmor = playerArmor;
    }
}
