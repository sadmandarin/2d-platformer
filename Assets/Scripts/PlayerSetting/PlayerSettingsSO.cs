using UnityEngine;

/// <summary>
/// SO с характеристиками игрока дл€ назначени€ в начале уровн€
/// </summary>
[CreateAssetMenu(fileName = "PlayerSettingsSO", menuName = "ScriptableObjects/PlayerSettings", order = 1)]
public class PlayerSettingsSO : ScriptableObject
{
    public int Hp;
    public int Mp;
    public WeaponsBase MeleeWeapon;
    public GameObject LongRangeWeapon;
    public GameObject Ability;
    public PlayerArmorBase PlayerArmor;

    /// <summary>
    /// ”становка текущего магического умени€
    /// </summary>
    /// <param name="ability"></param>
    public void SetHaracterictic(GameObject ability)
    {
        Ability = ability;
    }

    /// <summary>
    /// ”становка текущего вооружени€
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
    /// ”становка текущей брони
    /// </summary>
    /// <param name="playerArmor"></param>
    public void SetHaracterictic(PlayerArmorBase playerArmor)
    {
        PlayerArmor = playerArmor;
    }
}
