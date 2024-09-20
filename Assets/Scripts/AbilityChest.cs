using UnityEngine;

/// <summary>
/// Сундук умений для сбора на уровне
/// </summary>
public class AbilityChest : ChestBase
{
    private AbilitySOBase _ability;

    public override void OpenChest()
    {
        if (_isOpened)
        {
            Debug.Log("Сундук уже открыт");
        }
        else
        {
            if (_abilitiesManager.Keys > 0)
            {
                _abilitiesManager.AddAbility(_ability);
                Debug.Log($"Способность {_ability.AbilityName}");
                _isOpened = true;
            }
            else
                Debug.Log("Не хватает ключей"); 
        }

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
