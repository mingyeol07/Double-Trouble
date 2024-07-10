// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class EnemyE : Enemy
{
    private float angle;
    private readonly float rotationSpeed = 6f;
    [SerializeField] private Transform shootTransform;

    protected override void Update()
    {
        base.Update();
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        angle = PlayerManager.Instance.GetLookNearPlayerAngle(transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90), Time.deltaTime * rotationSpeed);
    }

    protected override void Shoot()
    {
        StartCoroutine(Co_DoubleShot());
    }

    private IEnumerator Co_DoubleShot()
    {
        BulletPoolManager.Instance.Spawn(BulletType.EnemyBullet2, shootTransform.position, angle + 90);
        yield return null;
    }
}