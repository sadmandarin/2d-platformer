using System.Collections.Generic;
using UnityEngine;

public class AbilitiesStorageManager : MonoBehaviour
{
    [SerializeField] private List<ChestBase> _chest;
    [SerializeField] private List<Key> _keysList;
    [SerializeField] private WeaponsAndAbilitiesStorage _storage;
    private int _keys;

    public int Keys {  get { return _keys; } private set { _keys = value; } }

    private void Awake()
    {
        foreach (var chest in _chest)
        {
            chest.SetAbilityManager(this);
        }

        foreach (var key in _keysList)
        {
            key.SetAbilityManager(this);
        }
    }

    public void AddKeys()
    {
        Keys++;

        Debug.Log($"Ключей собрано {Keys}");
    }

    public void AddWeapon(WeaponsBase weapon)
    {
        if (weapon != null)
            _storage.AddWeapon(weapon);
    }

    public void AddAbility(AbilitySOBase ability)
    {
        if (ability != null)
            _storage.AddAbility(ability);
        
    }
}
