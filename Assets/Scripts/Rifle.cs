using UnityEngine;

public class Rifle : WeaponBase
{

    private void Start()
    {

    }

    protected override void Attack(Vector2 direction)
    {
        EnableMuzzle();
        Instantiate(projectile, shootPoint.position, Quaternion.identity).GetComponent<Projectile>().direction = direction;
    }

}
