using UnityEngine;

public class Key : MonoBehaviour
{
    private OnLevelChestsStorageManager _abilitiesmanager;

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
