// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class EnemyBoss : Enemy
{
    protected override void Shot()
    {
        for (int i = 0; i < 360; i += 13)
        {
            BulletPoolManager.Instance.Spawn(BulletType.EnemyBullet, transform.position, i).GetComponent<Bullet>().SetSpeed(3);
        }
    }
    protected override IEnumerator Co_Destroy()
    {
        yield return null;
    }
}
