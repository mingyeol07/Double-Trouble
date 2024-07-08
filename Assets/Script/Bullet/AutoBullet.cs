using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bullet�� ��ӹ��� AutoBullet ��ũ��Ʈ
/// </summary>
public class AutoBullet : Bullet
{
    [SerializeField] private float rotationSpeed;
   [SerializeField]  private Transform closeEnemyTransform = null; // �� ��ġ�� ��Ÿ���� Transform ����

    protected override void Update()
    {
        base.Update();
        if (closeEnemyTransform != null)
        {
            if(closeEnemyTransform.gameObject.activeSelf != false)
            {
                FindCloseEnemy();
            }
            else
            {
                SetEnemyTransform();
            }
        }
    }

    /// <summary>
    /// ���� ��ġ�� ������ ���´�.
    /// </summary>
    private void FindCloseEnemy()
    {
        Vector2 direction = (Vector2)closeEnemyTransform.position - (Vector2)transform.position;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ȸ���� �������ϰ� ����
        Quaternion targetRotation = Quaternion.AngleAxis(rotZ - 90, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    /// <summary>
    /// ����� ���� ��ġ�� ã�´�.
    /// </summary>
    private Transform CloseTargetPosition(Collider2D[] colliders)
    {
        float distance = float.MaxValue;
        Transform closestTransform = null;

        for (int i = 0; i < colliders.Length; i++)
        {
            float colliderDistance = Vector2.Distance(colliders[i].transform.position, transform.position);
            if (distance > colliderDistance)
            {
                distance = colliderDistance;
                closestTransform = colliders[i].transform;
            }
        }

        return closestTransform;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SetEnemyTransform();
    }

    private void SetEnemyTransform()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 100f, LayerMask.GetMask("Enemy"));
        if (colliders.Length == 0)
        {
            closeEnemyTransform = null; // ���� ���� �� ó��
            return;
        }

        closeEnemyTransform = CloseTargetPosition(colliders);
    }
}
