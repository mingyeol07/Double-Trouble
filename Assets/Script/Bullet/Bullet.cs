using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletType type;

    [SerializeField] private int damage;
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rigid.velocity = Vector2.up * moveSpeed;
    }

    public int GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        BulletPoolManager.Instance.DeSpawn(type, this.gameObject);
    }

    private void OnEnable()
    {
        StartCoroutine(DistroyDelay());
    }

    private IEnumerator DistroyDelay()
    {
        yield return new WaitForSeconds(2f);
        Destroy();
    }
}
