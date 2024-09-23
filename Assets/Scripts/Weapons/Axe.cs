using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Описание оружия топора 
/// </summary>
[CreateAssetMenu(fileName = "Axe", menuName = "ScriptableObjects/Weapons/Axe", order = 1)]
public class Axe : WeaponsBase
{
    public float AttackRange;

    public override void QuickAttack(Transform attackPoint, MonoBehaviour owner)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<DefaultEnemy>())
                enemy.GetComponent<DefaultEnemy>().TakeDamage(QuickAttackDamage);
        }

        Debug.Log("Attack by Sword " + QuickAttackDamage);
    }

    public override void StrongAttack(Transform attackPoint, MonoBehaviour owner)
    {
        throw new System.NotImplementedException();
    }
}
