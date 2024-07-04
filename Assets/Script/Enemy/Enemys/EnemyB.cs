// # Systems
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;


// # Unity
using UnityEngine;
/// <summary>
/// 회전하며 예측불가능하게 총알을 쏘는 Enemy
/// </summary>
public class EnemyB : Enemy
{
    [SerializeField] private Transform[] shootTransform;
    [SerializeField] private float rotationSpeed;

    protected override void Update()
    {
        base.Update();
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    protected override void Shot()
    {
       for (int i = 0; i < shootTransform.Length; i++)
        {
            BulletPoolManager.Instance.Spawn(BulletType.EnemyBullet, shootTransform[i].position, shootTransform[i].eulerAngles.z);
        }
    }
}
