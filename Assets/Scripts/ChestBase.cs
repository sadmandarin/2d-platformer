using UnityEngine;

public abstract class ChestBase : MonoBehaviour
{
    protected AbilitiesStorageManager _abilitiesManager;
    protected bool _isOpened;

    public abstract void OpenChest();
    public void SetAbilityManager(AbilitiesStorageManager abilityManager)
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
