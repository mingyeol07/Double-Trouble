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

    protected override void Shoot()
    {
       for (int i = 0; i < shootTransform.Length; i++)
        {
            BulletPoolManager.Instance.Spawn(BulletType.EnemyBullet, shootTransform[i].position, shootTransform[i].eulerAngles.z);
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
