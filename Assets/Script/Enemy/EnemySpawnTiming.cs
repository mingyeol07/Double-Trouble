// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class EnemySpawnTiming : MonoBehaviour
{
    [SerializeField] private float maxTime;
    [SerializeField] private float playTime;
    [SerializeField] private GameObject bossA;
    private bool isSpawn;

    private void Update()
    {
        if (playTime < maxTime)  playTime += Time.deltaTime;

        if (playTime < maxTime && !isSpawn)
        {
            if((int)playTime == 10)
            {
                StartCoroutine(EnemySpawnManager.Instance.EnemySpawnLeft(EnemyType.EnemyA));
                StartCoroutine(EnemySpawnManager.Instance.EnemySpawnRight(EnemyType.EnemyA));
                StartCoroutine(SpawnWait());
            }

            if ((int)playTime == 20)
            {
                StartCoroutine(EnemySpawnManager.Instance.EnemySpawnFront(EnemyType.EnemyC));
                StartCoroutine(SpawnWait());
            }

            if ((int)playTime == 30)
            {
                StartCoroutine(EnemySpawnManager.Instance.EnemySpawnCross(EnemyType.EnemyB));
                StartCoroutine(SpawnWait());
            }

            if ((int)playTime == 40)
            {
                Instantiate(bossA);
                StartCoroutine(SpawnWait());
            }

            if ((int)playTime == 50)
            {

                StartCoroutine(SpawnWait());
            }

            if ((int)playTime == 60)
            {

                StartCoroutine(SpawnWait());
            }
        }
    }

    private IEnumerator SpawnWait()
    {
        isSpawn = true;
        yield   return  new WaitForSeconds(1.1f);
        isSpawn = false;
    }
}
