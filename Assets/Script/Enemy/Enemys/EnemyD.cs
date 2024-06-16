// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class EnemyD : Enemy
{
    [SerializeField] private Transform[] shootTransform;

    protected override void Shot()
    {
        for (int i = 0; i < shootTransform.Length; i++)
        {
            BulletPoolManager.Instance.Spawn(BulletType.EnemyBullet, shootTransform[i].position, 180);
        }
    }
}
