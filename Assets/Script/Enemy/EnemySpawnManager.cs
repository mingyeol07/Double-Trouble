using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private Vector2[] spawnPoint; // 왼쪽부터 
    [SerializeField] private EnemyWayPointData[] leftWayPoint;
    [SerializeField] private EnemyWayPointData[] rightWayPoint;
    [SerializeField] private EnemyWayPointData[] frontWayPoint;

    public void EnemySpawnPattern_1(EnemyType enemyType)
    {
        //                                                                                                    기말대비 방과후 수학문제 푸는중 
    }

    public void EnemySpawnPattern_2()
    {

    }

    public void EnemySpawnPattern_3()
    {

    }

    public void EnemySpawnPattern_4()
    {

    }
    public void EnemySpawnPattern_5()
    {

    }

    public void EnemySpawnPattern_6()
    {

    }

    private void EnemySpawn(EnemyType enemyType, float moveTime, Vector2 startPosition, Vector2 endPosition)
    {
        GameObject enemy = EnemyPoolManager.Instance.Spawn(enemyType);
        enemy.GetComponent<Enemy>().StartMove(moveTime, startPosition, endPosition);
        StartCoroutine(StartShoot(moveTime, enemy.GetComponent<Enemy>()));
    }

    private IEnumerator StartShoot(float waitTime, Enemy enemy)
    {
        yield return new WaitForSeconds(waitTime);
        enemy.StartShoot();
    }
}
