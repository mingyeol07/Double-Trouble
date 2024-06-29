using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private float gameTime;
    private bool spawned;
    private StageData stageData;
    private StageJsonSave jsonSave;

    private void Start()
    {
        gameTime = 0;
        stageData = jsonSave.LoadData();
    }

    private void Update()
    {
        if(stageData.maxTime > gameTime) gameTime += Time.deltaTime;
        if(!spawned)TimeLineCheck();
    }

    private void TimeLineCheck()
    {
        if (Mathf.RoundToInt(gameTime) % 5 == 0)
        {
            spawned = true;
            StartCoroutine(GetTimeLineEnemy(Mathf.RoundToInt(gameTime)));
        }
    }

    private IEnumerator GetTimeLineEnemy(int time)
    {
        foreach(EnemySpawnData enemySpawnData in stageData.spawnDatas)
        {
            if(enemySpawnData.spawnTime == time)
            {
                SpawnEnemy(enemySpawnData);
            }
        }
        yield return new WaitForSeconds(4);
        spawned = false;
    }

    private void SpawnEnemy(EnemySpawnData spawnData)
    {
        Vector2 startPos = new Vector2(spawnData.startPos.x, spawnData.startPos.y);
        Vector2 endPos = new Vector2(spawnData.endPos.x, spawnData.endPos.y);

        GameObject enemy = EnemyPoolManager.Instance.Spawn(spawnData.enemyType);
        enemy.transform.position = startPos;

        enemy.GetComponent<Enemy>().StartMove(1, startPos, endPos);
    }
}
