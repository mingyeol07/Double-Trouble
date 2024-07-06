// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;
/// <summary>
/// ¾Æ·¡·Î ³ÐÀº ¹üÀ§·Î ÃÑ¾ËÀ» ½î´Â Enemy
/// </summary>
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

    protected override IEnumerator Co_StartMove(float moveTime, Vector2 startPosition, Vector2 endPosition)
    {
        StartShoot();
        float time = 0;

        while (time < moveTime)
        {
            time += Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, startPosition, time / moveTime);
            yield return null;
        }
    }
}
