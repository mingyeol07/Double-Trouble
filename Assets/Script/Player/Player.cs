using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int curHp;

    [SerializeField] private Transform shootPositionLeft;
    [SerializeField] private Transform shootPositionRight;

    [SerializeField] private float shootDelayTime;
    [SerializeField] private BulletType bulletType;

    protected virtual void Start()
    {
        curHp = maxHp;
    }

    protected virtual void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("EnemyBeam"))
        {
            HpDown();
        }
    }

    private IEnumerator ShotDelay()
    {
        while(true)
        {
            ShotBullet();
            yield return new WaitForSeconds(shootDelayTime);
        }
    }

    protected virtual void ShotBullet()
    {
        BulletPoolManager.Instance.Spawn(bulletType, shootPositionLeft.position, 0);
        BulletPoolManager.Instance.Spawn(bulletType, shootPositionRight.position, 0);
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
        StartCoroutine(ShotDelay());
    }
}
