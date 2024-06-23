// # Systems
using System.Collections;
using Unity.VisualScripting;


// # Unity
using UnityEngine;

public class EnemyBoss : Enemy
{
    [SerializeField] private BossBarrier barrierLeft;
    [SerializeField] private BossBarrier barrierRight;

    [SerializeField] private Transform[] leftShootTransform;
    [SerializeField] private Transform[] rightShootTransform;

    protected override void Shot()
    {
        StartCoroutine(RandomPattern());
    }

    private IEnumerator RandomPattern()
    {
        int ranNum = Random.Range(0, 3);

        switch (ranNum)
        {
            case 0:
                StartCoroutine(CircleShoot());
                break;
            case 1:
                StartCoroutine(LeftShoot());
                break;
            case 2:
                StartCoroutine(RightShoot());
                break;
        }

        yield return null;
    }

    private IEnumerator CircleShoot()
    {
        for (int i = 0; i < 360; i += 13)
        {
            BulletPoolManager.Instance.Spawn(BulletType.EnemyBullet, transform.position, i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator LeftShoot()
    {
        for(int i = 0; i< leftShootTransform.Length ; i++)
        {
            BulletPoolManager.Instance.Spawn(BulletType.EnemyBullet, leftShootTransform[i].position, 180);
            yield return null;
        }
    }

    private IEnumerator RightShoot()
    {
        for (int i = 0; i < leftShootTransform.Length; i++)
        {
            BulletPoolManager.Instance.Spawn(BulletType.EnemyBullet, rightShootTransform[i].position, 180);
            yield return null;
        }
    }

    private void SpawnInterceptor()
    {

    }

    private void BarrierPattern()
    {
        barrierLeft.gameObject.SetActive(true);
        barrierRight.gameObject.SetActive(true);

        if(barrierLeft.GetOnLeftBarriering() && barrierRight.GetOnRightRarriering())
        {

        }
    }

    protected override IEnumerator Co_Destroy()
    {
        yield return null;
    }
}
