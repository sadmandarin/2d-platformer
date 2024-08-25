using UnityEngine;

public class WeaponChest : ChestBase
{
    [SerializeField] private WeaponsBase _weapon;

    public override void OpenChest()
    {
        if (_isOpened)
        {
            Debug.Log("������ ��� ������");
        }
        else
        {
            if (_abilitiesManager.Keys > 0)
            {
                _abilitiesManager.AddWeapon(_weapon);
                Debug.Log($"������ {_weapon.Name}");
                _isOpened = true;
            }
            else
                Debug.Log("�� ������� ������");
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
