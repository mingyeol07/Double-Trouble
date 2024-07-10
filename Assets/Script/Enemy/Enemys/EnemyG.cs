// # Systems
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;


// # Unity
using UnityEngine;

public class EnemyG : Enemy
{
    private Vector3[] poses = new Vector3[4];

    protected override void Shoot()
    {
        return;
    }

    protected override IEnumerator Co_StartMove(float moveTime, Vector2 startPosition, Vector2 endPosition)
    {
        poses[0] = startPosition;
        poses[1] = Vector3.zero;
        poses[2] = Vector3.zero;
        poses[3] = endPosition;

        float time = 0;
        moveTime = 5;
        while (time < moveTime)
        {
            time += Time.deltaTime;
            float t = time / moveTime;  // 비율 계산
            transform.position = cubicBezierVec(poses[0], poses[1], poses[2], poses[3], t);
            yield return null;
        }

        transform.position = endPosition;  // 이동이 끝나면 정확한 목표 위치로 설정
    }

    Vector3 cubicBezierVec(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        var ab = Vector3.Lerp(a, b, t);
        var bc = Vector3.Lerp(b, c, t);
        var cd = Vector3.Lerp(c, d, t);

        var abbc = Vector3.Lerp(ab, bc, t);
        var bccd = Vector3.Lerp(bc, cd, t);

        return Vector3.Lerp(abbc, bccd, t);
    }
}