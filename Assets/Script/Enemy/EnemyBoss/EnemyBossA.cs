// # Systems
using System.Collections;
using System.Linq.Expressions;
using Unity.VisualScripting;


// # Unity
using UnityEngine;

public class EnemyBossA : Enemy
{
    [SerializeField] private BossBarrier barrierLeft;
    [SerializeField] private BossBarrier barrierRight;

    [SerializeField] private Transform[] leftShootTransform;
    [SerializeField] private Transform[] rightShootTransform;

    [SerializeField] private GameObject centerBeamWarning;
    [SerializeField] private GameObject areaBeamWarning;

    [SerializeField] private GameObject centerBeam;
    [SerializeField] private GameObject areaBeam;

    private void Start()
    {
        StartCoroutine(Co_Shoot());
    }

    protected IEnumerator Co_Shoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(3);
        }
    }


    protected override void Shoot()
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
                StartCoroutine(RightShoot());
                break;
            case 2:
                StartCoroutine(LazerShoot());
                break;
        }

        yield return null;
    }

    private IEnumerator LazerShoot()
    {
        centerBeamWarning.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        centerBeamWarning.SetActive(false);
        yield return new WaitForSeconds(1f);
        centerBeam.SetActive(true);
        yield return new WaitForSeconds(1f);
        centerBeam.SetActive(false);
    }

    private IEnumerator CircleShoot()
    {
        for (int i = 7; i < 360; i += 13)
        {

            BulletPoolManager.Instance.Spawn(BulletType.EnemyBullet, transform.position, i);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 360; i += 13)
        {
            BulletPoolManager.Instance.Spawn(BulletType.EnemyBullet, transform.position, i);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        for (int i = 7; i < 360; i += 13)
        {
            BulletPoolManager.Instance.Spawn(BulletType.EnemyBullet, transform.position, i);
            yield return null;
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

    private IEnumerator BarrierPattern()
    {
        barrierLeft.gameObject.SetActive(true);
        barrierRight.gameObject.SetActive(true);

        areaBeamWarning.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        areaBeamWarning.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        areaBeam.SetActive(true);
        if (!barrierLeft.GetOnLeftBarriering() || !barrierRight.GetOnRightRarriering())
        {
            PlayerManager.Instance.player_L.HpDown();
            PlayerManager.Instance.player_R.HpDown();
        }
        yield return new WaitForSeconds(1f);
        barrierLeft.gameObject.SetActive(false);
        barrierRight.gameObject.SetActive(false);

        areaBeam.SetActive(false);
    }

    protected override IEnumerator Co_Destroy()
    {
        Destroy(gameObject);
        PlayerManager.Instance.Stage1Clear();
        yield return null;
    }
}
