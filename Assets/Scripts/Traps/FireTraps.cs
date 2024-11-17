using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTraps : TrapsBase
{
    private Player _player;
    private Coroutine _coroutine;
    private float _period = 1;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            _player = collision.gameObject.GetComponent<Player>();
            TrapAction();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            _player = null;
        }
    }

    protected override void TrapAction()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(RepeatingDamage());
        }
    }

    private IEnumerator RepeatingDamage()
    {
        while (_player != null)
        {
            _player.TakeDamage(_damage, transform);

            Debug.Log("Damage" + _damage);

            yield return new WaitForSeconds(_period);
        }
        _coroutine = null;
    }
}
