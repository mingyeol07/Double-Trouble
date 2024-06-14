using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmall : Enemy
{
    protected override void Shoot()
    {
        float angle = PlayerManager.Instance.GetLookNearPlayerAngle(transform.position);
        BulletPoolManager.Instance.Spawn(BulletType.EnemyBullet, shootTransform.position, angle);
    }
}
