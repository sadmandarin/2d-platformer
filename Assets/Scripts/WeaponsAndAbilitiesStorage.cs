using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������� ���� ��������� 
/// </summary>
[CreateAssetMenu(fileName = "AbilitiesStorage", menuName = "ScriptableObjects/AbilitiesStorage", order = 1)]
public class WeaponsAndAbilitiesStorage : ScriptableObject
{
    [SerializeField] private List<AbilitySOBase> _abilitiesList;
    [SerializeField] private List<WeaponsBase> _weaponsList;
    [SerializeField] private List<WeaponsBase> _longRangeWeaponList;
    [SerializeField] private List<PlayerArmorBase> _armorList;

    /// <summary>
    /// ���������� ������ � ���������
    /// </summary>
    /// <param name="weapon"></param>
    public void AddWeapon(WeaponsBase weapon)
    {
        if (weapon.IsRanged)
        {
            if (!_longRangeWeaponList.Contains(weapon))
            {
                _longRangeWeaponList.Add(weapon);
            }
        }
        else
        {
            if (!_weaponsList.Contains(weapon))
            {
                _weaponsList.Add(weapon);
            }
        }
    }

    /// <summary>
    /// ���������� ������ � ���������
    /// </summary>
    /// <param name="ability"></param>
    public void AddAbility(AbilitySOBase ability)
    {
        if (!_abilitiesList.Contains(ability))
        {
            _abilitiesList.Add(ability);
        }
    }

    /// <summary>
    /// ��������� ������� ��������� ������ �������� ���
    /// </summary>
    public List<WeaponsBase> GetAllLongRangeWeapons()
    {
        return _longRangeWeaponList;
    }

    /// <summary>
    /// ��������� ������� ��������� ��������� � ���������
    /// </summary>
    public List<T> GetListByType<T>()
    {
        if (typeof(T) == typeof(AbilitySOBase))
        {
            return _abilitiesList as List<T>;
        }
        else if (typeof(T) == typeof(WeaponsBase))
        {
            return _weaponsList as List<T>;
        }
        else if (typeof(T) == typeof(PlayerArmorBase))
        {
            return _armorList as List<T>;
        }
        else
        {
            Debug.LogError("Unknown type");
            return null;
        }
    }
}
