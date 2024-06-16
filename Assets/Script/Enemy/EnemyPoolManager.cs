using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance;

    [SerializeField] private List<EnemyData> enemyDatas = new();

    private Dictionary<EnemyType, GameObject> enemyPrefabDict;
    private Dictionary<EnemyType, Stack<GameObject>> stackDict;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        enemyPrefabDict = new Dictionary<EnemyType, GameObject>(); // ��ųʸ� �ʱ�ȭ
        stackDict = new Dictionary<EnemyType, Stack<GameObject>>(); // ��ųʸ� �ʱ�ȭ

        int enemyCount = enemyDatas.Count;

        for (int i = 0; i < enemyCount; i++)
        {
            EnemyData enemyData = enemyDatas[i];
            enemyPrefabDict[enemyData.type] = enemyData.prefab;
            stackDict[enemyData.type] = new Stack<GameObject>(); // ������ �κ�

            for (int j = 0; j < enemyData.spawnSize; j++)
            {
                GameObject enemy = Instantiate(enemyData.prefab);
                enemy.SetActive(false);
                enemy.transform.parent = transform;
                stackDict[enemyData.type].Push(enemy); // ������ �κ�
            }
        }
    }

    private GameObject InstantiatePrefab(EnemyType enemyType)
    {
        GameObject enemy = Instantiate(enemyPrefabDict[enemyType]);
        stackDict[enemyType].Push(enemy);
        return enemy;
    }

    public GameObject Spawn(EnemyType enemyType)
    {
        GameObject enemy = null;

        if (stackDict[enemyType].Count > 0)
        {
            enemy = stackDict[enemyType].Pop();
        }
        else
        {
            enemy = InstantiatePrefab(enemyType);
        }

        enemy.SetActive(true);

        return enemy;
    }

    public void DeSpawn(EnemyType enemyType, GameObject enemy)
    {
        enemy.SetActive(false);
        stackDict[enemyType].Push(enemy);
    }
}
