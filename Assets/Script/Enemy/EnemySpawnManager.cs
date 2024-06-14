using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{


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
