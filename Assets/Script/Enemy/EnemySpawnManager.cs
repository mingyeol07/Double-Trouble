using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private int gameTime5X;
    private bool spawned;
    private StageData stageData;
    private StageJsonSave jsonSave;

    private void Start()
    {
        jsonSave = GetComponent<StageJsonSave>();
        stageData = jsonSave.LoadData();
        Debug.Log(stageData.maxTime);
        gameTime5X = 0;
        StartCoroutine(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
        
        while (stageData.maxTime > gameTime5X)
        {
          
            TimeLineCheck();
            yield return new WaitForSeconds(5f);
            gameTime5X += 5;
        }
    }

    private void TimeLineCheck()
    {
        spawned = true;
        StartCoroutine(GetTimeLineEnemy(gameTime5X));
    }

    private IEnumerator GetTimeLineEnemy(int time)
    {
        foreach(EnemySpawnData enemySpawnData in stageData.spawnDatas)
        {
            
            if (enemySpawnData.spawnTime == time)
            {
                
                SpawnEnemy(enemySpawnData);
            }
        }
        yield return new WaitForSeconds(3);
        spawned = false;
    }

    private void SpawnEnemy(EnemySpawnData spawnData)
    {
        Vector2 startPos = new Vector2(spawnData.startPos.x, spawnData.startPos.y);
        Vector2 endPos = new Vector2(spawnData.endPos.x, spawnData.endPos.y);

        GameObject enemy = EnemyPoolManager.Instance.Spawn(spawnData.enemyType);
        enemy.transform.position = startPos;

       enemy.GetComponent<Enemy>().StartMove(1, startPos, endPos);
        StartCoroutine(ShootLate(enemy.GetComponent<Enemy>(), 1));

    }

    private IEnumerator ShootLate(Enemy enemy, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        enemy.StartShoot();
    }
}
