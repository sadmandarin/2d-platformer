using UnityEngine;

public abstract class TrapsBase : MonoBehaviour
{
    [SerializeField] protected int _damage;

    protected abstract void TrapAction();

    protected abstract void OnTriggerEnter2D(Collider2D collision);

}
