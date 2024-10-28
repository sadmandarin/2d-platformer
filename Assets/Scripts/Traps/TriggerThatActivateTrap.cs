using System.Collections;
using UnityEngine;

public class TriggerThatActivateTrap : MonoBehaviour
{
    [SerializeField] private TrapsBase _trap;
    [SerializeField] private TrapType _trapType;
    [SerializeField] private float _refreshTime;
    [SerializeField] private float _timeToActivateTrap;

    private Coroutine _coroutine;
    private bool _isWorked = false;

    private void Awake()
    {
        if (_trapType == TrapType.StoneFall)
        {
            _timeToActivateTrap = 2;
        }
        else if (_trapType == TrapType.StakeTrap)
        {
            _timeToActivateTrap = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            if (_coroutine == null && !_isWorked)
            {
                _coroutine = StartCoroutine(ActivateAfterDelay());
            }
        }
    }

    public IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(_timeToActivateTrap);

        ActivateTrap();
    }

    private void ActivateTrap()
    {
        if (!_trap.isActiveAndEnabled)
        {
            _trap.gameObject.SetActive(true);
            _isWorked = true;
            _coroutine = null;
            StartCoroutine(Refresh());
        }
    }

    private IEnumerator Refresh()
    {
        yield return new WaitForSeconds(_refreshTime);

        _isWorked = false;
    }

    public enum TrapType
    {
        StoneFall,
        StakeTrap
    }
}
