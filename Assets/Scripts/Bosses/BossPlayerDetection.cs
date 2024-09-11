using UnityEngine;

public class BossPlayerDetection : MonoBehaviour
{
    private BossBase _bossBase;

    private void Awake()
    {
        _bossBase = GetComponentInParent<BossBase>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            _bossBase.SetPlayer(collision.gameObject.GetComponent<Player>());
        }
    }
}
