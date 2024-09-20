using UnityEngine;

/// <summary>
/// ������ ������ ��� ����� �� ������
/// </summary>
public class AbilityChest : ChestBase
{
    private AbilitySOBase _ability;

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
                _abilitiesManager.AddAbility(_ability);
                Debug.Log($"����������� {_ability.AbilityName}");
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
