using UnityEngine;

/// <summary>
/// ����� ����� ��� �������� ������� � ��������
/// </summary>
public class Key : MonoBehaviour
{
    private OnLevelChestsStorageManager _abilitiesmanager;

    /// <summary>
    /// ��������� SO � ��������
    /// </summary>
    /// <param name="manager"></param>
    public void SetAbilityManager(OnLevelChestsStorageManager manager)
    {
        _abilitiesmanager = manager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            _abilitiesmanager.AddKeys();
            Destroy(gameObject);
        }
    }
}
