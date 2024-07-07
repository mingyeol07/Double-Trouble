// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;
/// <summary>
/// 아래로 넓은 범위로 총알을 쏘는 Enemy
/// </summary>
public class EnemyD : Enemy
{
    [SerializeField] private Transform[] shootTransform;

    protected override void Shoot()
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
        moveTime = 5;
        while (time < moveTime)
        {
            time += Time.deltaTime;
            float t = time / moveTime;  // 비율 계산
            transform.position = Vector2.Lerp(startPosition, endPosition, t);  // startPosition에서 endPosition으로 보간
            yield return null;
        }

        transform.position = endPosition;  // 이동이 끝나면 정확한 목표 위치로 설정
    }
}
