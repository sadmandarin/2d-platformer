using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilitiesStorage", menuName = "ScriptableObjects/AbilitiesStorage", order = 1)]
public class WeaponsAndAbilitiesStorage : ScriptableObject
{
    [SerializeField] private List<AbilitySOBase> _abilitiesList;
    [SerializeField] private List<WeaponsBase> _weaponsList;

    public void AddWeapon(WeaponsBase weapon)
    {
        _weaponsList.Add(weapon);
    }

    public void AddAbility(AbilitySOBase ability)
    {
        _abilitiesList.Add(ability);
    }

    public List<AbilitySOBase> GetAllAbilities()
    {
        return _abilitiesList;
    }

    public List<WeaponsBase> GetAllWeapons()
    {
        return _weaponsList;
    }
}
