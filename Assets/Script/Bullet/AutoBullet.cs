// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

/// <summary>
/// Bullet�� ��ӹ��� AutoBullet ��ũ��Ʈ
/// </summary>
public class AutoBullet : Bullet
{
    [SerializeField] private float rotationSpeed;

    protected override void Update()
    {
        base.Update();
        FindCloseEnemy();
    }

    /// <summary>
    /// ���� ��ġ�� ������ ���´�.
    /// </summary>
    private void FindCloseEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 20f, LayerMask.GetMask("Enemy"));
        if (colliders.Length == 0)
        {
            return; // No enemies in range
        }

        Vector2 closeEnemyPosition = CloseTargetPosition(colliders);

        Vector2 direction = closeEnemyPosition - (Vector2)transform.position;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ȸ���� �������ϰ� ����
        Quaternion targetRotation = Quaternion.AngleAxis(rotZ - 90, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    /// <summary>
    /// ����� ���� ��ġ�� ã�´�.
    /// </summary>
    private Vector2 CloseTargetPosition(Collider2D[] collider)
    {
        float distance = float.MaxValue;

        for (int i =0; i< collider.Length; i++)
        {
            float colliderDistance = Vector2.Distance(collider[i].transform.position, transform.position);
            if (distance > colliderDistance)
            {
                distance = colliderDistance;
                return collider[i].transform.position;
            }
        }
        return Vector2.zero;
    }
}
