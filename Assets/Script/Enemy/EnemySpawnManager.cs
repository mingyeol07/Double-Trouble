using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance;

    [SerializeField] private EnemyWayPointData[] leftWayPoint;
    [SerializeField] private EnemyWayPointData[] rightWayPoint;
    [SerializeField] private EnemyWayPointData[] frontWayPoint;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator EnemySpawnLeft(EnemyType enemy)
    {
        for (int i = 0; i < leftWayPoint.Length; i++) {
            EnemySpawn(enemy, 1f, leftWayPoint[i].StartPoint, leftWayPoint[i].EndPoint);
            yield return null;
        }

    }

    public IEnumerator EnemySpawnRight(EnemyType enemy)
    {
        for (int i = 0; i < rightWayPoint.Length; i++)
        {
            EnemySpawn(enemy, 1f, rightWayPoint[i].StartPoint, rightWayPoint[i].EndPoint);
            yield return null;
        }
    }

    public IEnumerator EnemySpawnFront(EnemyType enemy)
    {
        for (int i = 0; i < rightWayPoint.Length; i++)
        {
            EnemySpawn(enemy, 1f, frontWayPoint[i].StartPoint, frontWayPoint[i].EndPoint);
            yield return null;
        }
    }

    public IEnumerator EnemySpawnCross(EnemyType enemy)
    {
        EnemySpawn(enemy, 5f, leftWayPoint[2].StartPoint, rightWayPoint[0].EndPoint);
        EnemySpawn(enemy, 5f, rightWayPoint[0].StartPoint, leftWayPoint[2].EndPoint);

        yield return null;
    }

    private void EnemySpawn(EnemyType enemyType, float moveTime, Vector2 startPosition, Vector2 endPosition)
    {
        GameObject enemy = EnemyPoolManager.Instance.Spawn(enemyType);
        enemy.GetComponent<Enemy>().StartMove(moveTime, startPosition, endPosition);

        if (enemyType == EnemyType.EnemyB) StartCoroutine(StartShoot(0, enemy.GetComponent<Enemy>()));
        else StartCoroutine(StartShoot(moveTime, enemy.GetComponent<Enemy>()));
    }

    private IEnumerator StartShoot(float waitTime, Enemy enemy)
    {
        yield return new WaitForSeconds(waitTime);
        enemy.StartShoot();
    }
}
