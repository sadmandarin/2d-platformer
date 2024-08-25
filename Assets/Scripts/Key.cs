using UnityEngine;

public class Key : MonoBehaviour
{
    private AbilitiesStorageManager _abilitiesmanager;

    public void SetAbilityManager(AbilitiesStorageManager manager)
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
