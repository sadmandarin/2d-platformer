using UnityEngine;

/// <summary>
/// ������� ����� ��� ������� � ��������/�������
/// </summary>
public abstract class ChestBase : MonoBehaviour
{
    protected OnLevelChestsStorageManager _abilitiesManager;
    protected bool _isOpened;

    /// <summary>
    /// �������� �������
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
