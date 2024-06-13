using System.Collections;
using System.Xml;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
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

    }

    protected virtual void OnEnable()
    {
        StartCoroutine(ShootDelay());
    }
}
