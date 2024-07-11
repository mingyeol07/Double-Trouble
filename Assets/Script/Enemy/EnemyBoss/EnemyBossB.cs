using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBossB : Enemy
{
    [SerializeField] private GameObject slash;
    [SerializeField] private Transform[] bulletTransform;
    [SerializeField] private int testRandomIndex;

    private Vector2 leftStartPosition = new Vector2(-10, 5);
    private Vector2 leftEndPosition = new Vector2(-10, -5);

    private Vector2 rightStartPosition = new Vector2(10, -5);
    private Vector2 rightEndPosition = new Vector2(10, 5);

    private Vector2[] bezierVec = { new Vector2(-11, -3), new Vector2(-4.5f, 3), new Vector2(4.5f, -8), new Vector2(11, -3) };

    private void Start()
    {
        StartCoroutine(Co_Shoot());
    }

    protected IEnumerator Co_Shoot()
    {
        yield return new WaitForSeconds(1f);
        Shoot();
    }

    protected override void Shoot()
    {
        StartCoroutine(RandomPattern());
    }

    private IEnumerator RandomPattern()
    {
        int ranNum;
        if (testRandomIndex == 0) ranNum = Random.Range(0, 3);
        else ranNum = testRandomIndex - 1;

        float waitTime = 3f;

        switch (ranNum)
        {
            case 0:
                SpawnEnemyGSide();
                break;
            case 1:
                SpawnEnemyGCenter();
                break;
            case 2:
                SpawnEnemyGCurve();
                break;
        }

        SpawnSlash();
        yield return new WaitForSeconds(waitTime);
        Shoot();
    }

    private void SpawnEnemyGSide()
    {
        GameObject enemy1 = EnemyPoolManager.Instance.Spawn(EnemyType.EnemyG);
        GameObject enemy2 = EnemyPoolManager.Instance.Spawn(EnemyType.EnemyG);

        enemy1.GetComponent<Enemy>().StartMove(5, leftStartPosition, leftEndPosition);
        enemy2.GetComponent<Enemy>().StartMove(5, rightStartPosition, rightEndPosition);
    }

    private void SpawnEnemyGCenter()
    {
        GameObject enemy = EnemyPoolManager.Instance.Spawn(EnemyType.EnemyG);

        enemy.GetComponent<EnemyG>().CustomBezierVecs(bezierVec[0], bezierVec[1], bezierVec[2], bezierVec[3], 5);
    }

    private void SpawnEnemyGCurve()
    {
        GameObject enemy1 = EnemyPoolManager.Instance.Spawn(EnemyType.EnemyG);
        GameObject enemy2 = EnemyPoolManager.Instance.Spawn(EnemyType.EnemyG);

        Vector2 middlePosition_1 = new Vector2(bulletTransform[0].position.x + (Mathf.Abs(bulletTransform[0].position.x - bulletTransform[2].position.x) / 2), -8);
        Vector2 middlePosition_2 = new Vector2(bulletTransform[1].position.x + (Mathf.Abs(bulletTransform[3].position.x - bulletTransform[1].position.x) / 2), -8);

        enemy1.GetComponent<EnemyG>().CustomBezierVecs(bulletTransform[0].position, middlePosition_1, middlePosition_1, bulletTransform[2].position, 5);
        enemy2.GetComponent<EnemyG>().CustomBezierVecs(bulletTransform[3].position, middlePosition_2, middlePosition_2, bulletTransform[1].position, 5);
    }

    private void SpawnSlash()
    {
        int ranSpawnPos = Random.Range(0, 4);

        GameObject go = Instantiate(slash);
        go.transform.position = bulletTransform[ranSpawnPos].position;
        Destroy(go, 5f);
    }

    protected override IEnumerator Co_Destroy()
    {
        Destroy(gameObject);
        PlayerManager.Instance.Stage2Clear();
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
