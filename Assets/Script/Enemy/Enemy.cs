using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int curHp;

    private void Start()
    {
        curHp = maxHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HpDown(collision.gameObject.GetComponent<Bullet>().GetDamage());
        }
    }

    private void HpDown(int damage)
    {
        curHp -= damage;
        if (curHp <= 0) Destroy();
    }
    
    private void Destroy()
    {

    }
}
