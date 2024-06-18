// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class AutoBullet : Bullet
{
    protected override void Update()
    {
        
    }

    private void FindCloseEnemy()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 20f, 7);
        Vector2 closeEnemyPosition = CloseTargetPosition(collider);
        // 따라가기
    }

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
