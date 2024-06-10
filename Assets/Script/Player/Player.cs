using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int curHp;

    [SerializeField] private Transform shootPositionLeft;
    [SerializeField] private Transform shootPositionRight;

    private WaitForSeconds shootDelay = new WaitForSeconds(0.2f);

    protected virtual void Start()
    {
        curHp = maxHp;
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

    private IEnumerator ShootDelay()
    {
        ShootBullet();
        yield return shootDelay;
        StartCoroutine(ShootDelay());
    }

    private void ShootBullet()
    {
        GameObject bulletLeft = BulletPoolManager.Instance.Spawn(BulletType.Bullet, transform);
        bulletLeft.transform.position = shootPositionLeft.position;
        bulletLeft.transform.eulerAngles = Vector2.up;

        GameObject bulletRight = BulletPoolManager.Instance.Spawn(BulletType.Bullet, transform);
        bulletRight.transform.position = shootPositionRight.position;
        bulletRight.transform.eulerAngles = Vector2.up;
    }

    private void ShootPowerUpBullet()
    {
        BulletPoolManager.Instance.Spawn(BulletType.PowerUpBullet, transform);
    }

    private void PowerUp()
    {

    }

    private void HpDown()
    {
        curHp--;
        if (curHp <= 0)
        {
            GameOver();
        }
    }

    protected virtual void GameOver()
    {
        // destroy
    }
}
