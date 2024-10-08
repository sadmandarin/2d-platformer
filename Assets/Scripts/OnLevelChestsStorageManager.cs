using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������� ��������� � ������� �� ������
/// </summary>
public class OnLevelChestsStorageManager : MonoBehaviour
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

    /// <summary>
    /// ���������� ������ ��� ��������
    /// </summary>
    public void AddKeys()
    {
        Keys++;

        Debug.Log($"������ ������� {Keys}");
    }

    /// <summary>
    /// ������ ������ �� �������
    /// </summary>
    /// <param name="weapon">������</param>
    public void AddWeapon(WeaponsBase weapon)
    {
        if (weapon != null)
            _storage.AddWeapon(weapon);
    }

    /// <summary>
    /// ������ ������ �� �������
    /// </summary>
    /// <param name="ability">������</param>
    public void AddAbility(AbilitySOBase ability)
    {
        if (ability != null)
            _storage.AddAbility(ability);
        
    }
}
