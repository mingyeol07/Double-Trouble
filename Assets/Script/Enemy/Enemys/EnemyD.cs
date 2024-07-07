// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;
/// <summary>
/// �Ʒ��� ���� ������ �Ѿ��� ��� Enemy
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
            float t = time / moveTime;  // ���� ���
            transform.position = Vector2.Lerp(startPosition, endPosition, t);  // startPosition���� endPosition���� ����
            yield return null;
        }

        transform.position = endPosition;  // �̵��� ������ ��Ȯ�� ��ǥ ��ġ�� ����
    }
}
