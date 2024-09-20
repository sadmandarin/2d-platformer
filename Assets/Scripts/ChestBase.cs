using UnityEngine;

/// <summary>
/// Базовый класс для сундука с умениями/оружием
/// </summary>
public abstract class ChestBase : MonoBehaviour
{
    protected OnLevelChestsStorageManager _abilitiesManager;
    protected bool _isOpened;

    /// <summary>
    /// Открытие сундука
    /// </summary>
    public abstract void OpenChest();
    public void SetAbilityManager(OnLevelChestsStorageManager abilityManager)
    {
        _abilitiesManager = abilityManager;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isOpened)
        {
            if (collision.gameObject.GetComponent<Player>())
            {
                OpenChest();
            }
        }
    }
}
