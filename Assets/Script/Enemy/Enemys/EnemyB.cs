// # Systems
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;


// # Unity
using UnityEngine;

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
