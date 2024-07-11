// # Systems
using System.Collections;

// # Unity
using UnityEngine;

public class EnemyG : Enemy
{
    float angle;
    private readonly float rotationSpeed = 6f;
    private Vector3[] poses = new Vector3[4];

    protected override void Shoot()
    {
        return;
    }

    public void CustomBezierVecs(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float moveTime)
    {
        StartCoroutine(Co_StartMove(moveTime, a, d));

        poses[0] = a;
        poses[1] = b;
        poses[2] = c;
        poses[3] = d;
    }

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

        StartCoroutine(Co_Destroy());
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