using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
/// <summary>
/// 플레이어를 바라보며 주기적으로 총알을 쏘는 Enemy
/// </summary>
public class EnemyA : Enemy
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
        angle = PlayerManager.Instance.GetLookNearPlayerAngle(transform.position) ;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90), Time.deltaTime * rotationSpeed);
    }

    protected override void Shot()
    {
        StartCoroutine(Co_DoubleShot());
    }

    private IEnumerator Co_DoubleShot()
    {
        BulletPoolManager.Instance.Spawn(BulletType.EnemyBullet, shootTransform.position, angle +90);
        yield return new WaitForSeconds(0.2f);
        BulletPoolManager.Instance.Spawn(BulletType.EnemyBullet, shootTransform.position, angle +90);
    }
}
