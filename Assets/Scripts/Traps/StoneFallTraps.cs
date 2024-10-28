using System.Collections;
using UnityEngine;

public class StoneFallTraps : TrapsBase
{
    private Vector2 _startPos;
    private float _timeToDeactivate = 0.4f;

    private void OnEnable()
    {
        TrapAction();
    }

    private void OnDisable()
    {
        transform.position = _startPos;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(_damage, transform);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.GetComponent<Ground>())
        {
            StartCoroutine(DeactivateTrap());
        }
    }

    private IEnumerator DeactivateTrap()
    {
        yield return new WaitForSeconds(_timeToDeactivate);

        gameObject.SetActive(false);
    }

    protected override void TrapAction()
    {
        _startPos = transform.position;
        _damage = 20;
    }
}
