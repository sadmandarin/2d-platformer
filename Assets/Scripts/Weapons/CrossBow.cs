using UnityEngine;

[CreateAssetMenu(fileName = "CrossBow", menuName = "ScriptableObjects/Weapons/CrossBow", order = 2)]
public class CrossBow : WeaponsBase
{
    public float ProjectileRange;
    public float ProjectileSpeed;
    public GameObject ProjectilePrefab;

    public override void QuickAttack(Transform attackpoint, MonoBehaviour owner)
    {
        GameObject projectile = Instantiate(ProjectilePrefab, attackpoint.position, owner.transform.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = owner.transform.right * ProjectileSpeed;

        Projectile proj = projectile.GetComponent<Projectile>();
        proj.SetProjectile(QuickAttackDamage, ProjectileRange);
    }

    public override void StrongAttack(Transform attackPoint, MonoBehaviour owner)
    {
        throw new System.NotImplementedException();
    }
}
