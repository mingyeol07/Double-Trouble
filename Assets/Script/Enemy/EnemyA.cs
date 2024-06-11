using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : Enemy
{
    protected override void Shoot()
    {
        float angle = PlayerManager.Instance.GetLookNearPlayerAngle(transform.position);
        GameObject bullet = BulletPoolManager.Instance.Spawn(BulletType.EnemyBullet, shootTransform.position, angle);
    }
}
