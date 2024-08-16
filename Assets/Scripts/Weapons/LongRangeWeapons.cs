using UnityEngine;

[CreateAssetMenu(fileName = "LongRangeWeapons", menuName = "ScriptableObjects/Weapons/LongRangeWeapons", order = 0)]
public class LongRangeWeapons : WeaponsBase
{
    public float ProjectileRange;
    public float ProjectileSpeed;

    protected override void Attack(int damage)
    {
        Debug.Log("Attack by LongRange " + damage);
    }
}
