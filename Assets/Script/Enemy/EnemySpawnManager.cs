using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// Json을 불러와 정보를 읽고 시간에 따라 Eenmy를 소환하는
/// </summary>
public class EnemySpawnManager : MonoBehaviour
{
    private int gameTime;
    private int stageIndex;
    private StageData stageData;
    private StageJsonSave jsonSave;
    private StageBackGroundManager backGroundManager;

    private void Start()
    {
        jsonSave = GetComponent<StageJsonSave>();
        stageData = jsonSave.LoadData(stageIndex);

        gameTime = 0;
        StartCoroutine(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
        while (stageData.maxTime > gameTime)
        {
            yield return new WaitForSeconds(1f);
            gameTime += 1;
            TimeLineCheck(gameTime);
        }
    }

    private void TimeLineCheck(int time)
    {
        foreach (EnemySpawnData enemySpawnData in stageData.spawnDatas)
        {
            if (enemySpawnData.spawnTime == time)
            {
                SpawnEnemy(enemySpawnData);
            }
        }
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
