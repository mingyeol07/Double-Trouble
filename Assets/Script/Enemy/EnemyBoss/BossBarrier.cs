using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarrier : MonoBehaviour
{
    [SerializeField] private bool isLeftBarrier;
    [SerializeField] private EnemyBossA boss;

    private void Start()
    {
        boss = GetComponentInParent<EnemyBossA>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(isLeftBarrier)
            {
                Debug.Log("true");
                boss.SetIsBarrieringLeft(true);
            }
            else
            {
                Debug.Log("true");
                boss.SetIsBarrieringRight(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isLeftBarrier)
            {
                Debug.Log("False");
                boss.SetIsBarrieringLeft(false);
            }
            else
            {
                Debug.Log("False");
                boss.SetIsBarrieringRight(false);
            }
        }
    }
}
