using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapons", menuName = "ScriptableObjects/Weapons/MeleeWeapons", order = 1)]
public class MeleeWeapons : WeaponsBase
{
    public float AttackRange;

    protected override void Attack(int damage)
    {
        Debug.Log("Attack by Melee " + damage);
    }
}
