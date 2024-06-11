using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int curHp;

    [SerializeField] private Transform shootPositionLeft;
    [SerializeField] private Transform shootPositionRight;

    [SerializeField] private float shootDelayTime;

    private BulletType bulletType;

    protected virtual void Start()
    {
        curHp = maxHp;
        bulletType = BulletType.Bullet;
        StartCoroutine(ShootDelay());
    }

    protected virtual void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            HpDown();
        }
        else if (collision.gameObject.CompareTag("PowerUp"))
        {
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            
        }
    }

    private IEnumerator ShootDelay()
    {
        ShootBullet();
        yield return new WaitForSeconds(shootDelayTime);
        StartCoroutine(ShootDelay());
    }

    private void ShootBullet()
    {
        BulletPoolManager.Instance.Spawn(bulletType, shootPositionLeft.position, -90);
        BulletPoolManager.Instance.Spawn(bulletType, shootPositionRight.position, -90);
    }

    private void ShootPowerUpBullet()
    {
        bulletType = BulletType.PowerUpBullet;
    }

    private void HpDown()
    {
        curHp--;
        if (curHp <= 0)
        {
            GameOver();
        }
    }

    protected abstract void GameOver();
}
