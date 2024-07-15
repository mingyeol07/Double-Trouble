// # Systems
using System.Collections;
using System.Linq.Expressions;
using Unity.VisualScripting;


// # Unity
using UnityEngine;

public class EnemyBossA : Enemy
{
    [SerializeField] private Transform[] leftShootTransform;
    [SerializeField] private Transform[] rightShootTransform;

    [SerializeField] private GameObject centerBeamWarning;
    [SerializeField] private GameObject areaBeamWarning;

    [SerializeField] private GameObject centerBeam;
    [SerializeField] private GameObject areaBeam;

    [SerializeField] private GameObject item;

    [SerializeField] private int testRandomIndex;

    private void Start()
    {
        StartCoroutine(Co_Shoot());
    }

    protected IEnumerator Co_Shoot()
    {
        yield return new WaitForSeconds(3f);
        Shoot();
    }

    protected override void Shoot()
    {
        StartCoroutine(RandomPattern());
    }

    private IEnumerator RandomPattern()
    {
        int ranNum;
        if (testRandomIndex == 0) ranNum = Random.Range(0, 4);
        else ranNum = testRandomIndex;

        float waitTime = 3f;

        switch (ranNum)
        {
            case 0:
                StartCoroutine(CircleShoot());
                waitTime = 2f;
                break;
            case 1:
                StartCoroutine(LeftShoot());
                StartCoroutine(RightShoot());
                waitTime = 1f;
                break;
            case 2:
                StartCoroutine(LazerShoot());
                waitTime = 2f;
                break;
            case 3:
                StartCoroutine(SpawnEnemy());
                waitTime = 1f;
                break;
        }

        yield return new WaitForSeconds(waitTime);
        Shoot();
    }

    private IEnumerator SpawnEnemy()
    {
        GameObject enemy1 =  EnemyPoolManager.Instance.Spawn(EnemyType.EnemyA);
        GameObject enemy2 = EnemyPoolManager.Instance.Spawn(EnemyType.EnemyA);

        enemy1.GetComponent<Enemy>().StartMove(3f, new Vector2(-6, 2), new Vector2(-6, -2));
        enemy2.GetComponent<Enemy>().StartMove(3f, new Vector2(6, 2), new Vector2(6, -2));

        yield return null;
    }

    private IEnumerator LazerShoot()
    {
        centerBeamWarning.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        centerBeamWarning.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        centerBeam.SetActive(true);
        yield return new WaitForSeconds(1f);
        centerBeam.SetActive(false);
    }

    private IEnumerator CircleShoot()
    {
        for (int i = 0; i < 360; i += 13)
        {
            BulletPoolManager.Instance.Spawn(BulletType.EnemyBullet, transform.position, i);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        for (int i = 6; i < 366; i += 13)
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

    protected override IEnumerator Co_Destroy()
    {
        Destroy(gameObject);
        PlayerManager.Instance.Stage1Clear();
        yield return null;
    }

    protected override IEnumerator Co_StartMove(float moveTime, Vector2 startPosition, Vector2 endPosition)
    {
        Vector2 velocity = Vector2.zero;
        float offset = 0.2f;

        transform.position = startPosition;

        while (Vector2.Distance((Vector2)transform.position, endPosition) > offset)
        {
            transform.position = Vector2.SmoothDamp(transform.position, endPosition, ref velocity, moveTime);
            yield return null;
        }
    }
}
