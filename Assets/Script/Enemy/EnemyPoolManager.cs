using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance;

    [SerializeField] private List<EnemyData> enemyDatas = new();

    private Dictionary<EnemyType, GameObject> enemyPrefabDict;
    private Dictionary<GameObject, Stack<GameObject>> stackDict;

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
        enemyPrefabDict = new Dictionary<EnemyType, GameObject>(); // 딕셔너리 초기화
        stackDict = new Dictionary<GameObject, Stack<GameObject>>(); // 딕셔너리 초기화

        int enemyCount = enemyDatas.Count;

        for (int i = 0; i < enemyCount; i++)
        {
            EnemyData enemyData = enemyDatas[i];
            enemyPrefabDict[enemyData.type] = enemyData.prefab;
            stackDict[enemyData.prefab] = new Stack<GameObject>(); // 수정된 부분

            for (int j = 0; j < 200; j++)
            {
                GameObject enemy = Instantiate(enemyData.prefab);
                enemy.SetActive(false);
                enemy.transform.parent = transform;
                stackDict[enemyData.prefab].Push(enemy); // 수정된 부분
            }
        }
    }

    private GameObject InstantiatePrefab(EnemyType enemyType)
    {
        GameObject enemy = Instantiate(enemyPrefabDict[enemyType]);
        stackDict[enemyPrefabDict[enemyType]].Push(enemy);
        enemy.transform.parent = transform;
        return enemy;
    }

    public GameObject Spawn(EnemyType enemyType, Transform parent)
    {
        GameObject enemy = null;

        if (stackDict[enemyPrefabDict[enemyType]].Count > 0)
        {
            enemy = stackDict[enemyPrefabDict[enemyType]].Pop();
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
        enemy.transform.parent = transform;
        stackDict[enemyPrefabDict[enemyType]].Push(enemy);
    }
}
