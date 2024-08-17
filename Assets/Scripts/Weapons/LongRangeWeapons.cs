using UnityEngine;

[CreateAssetMenu(fileName = "LongRangeWeapons", menuName = "ScriptableObjects/Weapons/LongRangeWeapons", order = 0)]
public class LongRangeWeapons : WeaponsBase
{
    public float ProjectileRange;
    public float ProjectileSpeed;

    public override void QuickAttack(Transform attackpoint, MonoBehaviour owner)
    {
        throw new System.NotImplementedException();
    }

    public override void StrongAttack(Transform attackPoint, MonoBehaviour owner)
    {
        throw new System.NotImplementedException();
    }
}
