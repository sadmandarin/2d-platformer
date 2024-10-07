using System.Collections;
using UnityEngine;

public abstract class ThrowingWeaponBase : MonoBehaviour
{
    [SerializeField] public float _throwingDistance;
    [SerializeField] public float _delay;

    protected float _damage;

    public void SetStats(float damage)
    {
        _damage = damage;
    }

    public abstract IEnumerator ThrowingTrajectory(int   direction, Vector2 attackPoint);
    protected abstract void OnTriggerEnter2D(Collider2D collision);
}
