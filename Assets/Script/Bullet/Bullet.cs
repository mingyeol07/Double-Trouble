using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 총알들의 부모 클래스
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletType type;

    [SerializeField] private int damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float destroyWaitTime;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        rigid.velocity = transform.up * moveSpeed;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && type != BulletType.EnemyBullet)
        {
            if(type == BulletType.LeftBullet)
            {
                PlayerManager.Instance.SetLeftPlayerGaugePlus();
                Destroy();
            }
            else if(type == BulletType.RightBullet)
            {
                PlayerManager.Instance.SetRightPlayerGaugePlus();
                Destroy();
            }
            else if(type == BulletType.Beam)
            {
                
            }
            else
            {
                Destroy();
            }
        }
        else if (collision.gameObject.CompareTag("Player") && type == BulletType.EnemyBullet)
        {
            Destroy();
        }
    }

    protected virtual void Destroy()
    {
        BulletPoolManager.Instance.DeSpawn(type, this.gameObject);
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(DistroyDelay());
    }

    private IEnumerator DistroyDelay()
    {
        yield return new WaitForSeconds(destroyWaitTime);
        Destroy();
    }
}
