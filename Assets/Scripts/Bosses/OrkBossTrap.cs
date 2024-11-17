using System.Collections;
using UnityEngine;

public class OrkBossTrap : MonoBehaviour
{
    private int _destoryDelay = 15;
    private OrkBoss _parent;

    private void OnEnable()
    {
        StartCoroutine(DestroyOnDelayEnd());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            ActivateTrap(collision.gameObject.GetComponent<Player>());
        }
    }

    private IEnumerator DestroyOnDelayEnd()
    {
        yield return new WaitForSeconds(_destoryDelay);

        OnDelayEnd();
    }

    private void OnDelayEnd()
    {
        _parent._currentTrapCount--;

        Destroy(gameObject);
    }

    private void ActivateTrap(Player player)
    {
        _parent._currentTrapCount--;

        Debug.Log("Попался");

        player.SetStunState(1.7f);

        Destroy(gameObject);
    }

    public void SetParent(OrkBoss parent)
    {
        _parent = parent;
    }
}
