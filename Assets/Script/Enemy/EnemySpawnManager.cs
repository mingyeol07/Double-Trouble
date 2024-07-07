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
    [SerializeField] private int stageIndex;
    private StageData stageData;
    private StageJsonSave jsonSave;
    private StageBackGroundManager backGroundManager;
    [SerializeField] private GameObject bossA;
    [SerializeField] private GameObject bossB;

    private void Start()
    {
        backGroundManager = GetComponent<StageBackGroundManager>();
        jsonSave = GetComponent<StageJsonSave>();
        stageData = jsonSave.LoadData(stageIndex);
        backGroundManager.SetStageBackGround(stageIndex);
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

        GameObject enemy = null;

        if (spawnData.enemyType == EnemyType.EnemyBossA)
        {
            enemy = bossA;
        }
       else if(spawnData.enemyType == EnemyType.EnemyBossB)
        {
            enemy = bossB;
        }
        else
        {
            enemy = EnemyPoolManager.Instance.Spawn(spawnData.enemyType);
            enemy.transform.position = startPos;
        }

        enemy.SetActive(true);
        enemy.GetComponent<Enemy>().StartMove(1, startPos, endPos);
    }
}
