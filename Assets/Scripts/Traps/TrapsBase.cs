using System.Collections;
using UnityEngine;

public abstract class TrapsBase : MonoBehaviour
{
    protected Player _player;
    protected float _timeToActivateTrap = 2;

    public IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(_timeToActivateTrap);

        ActivateTrap();
    }

    protected abstract void ActivateTrap();
}
