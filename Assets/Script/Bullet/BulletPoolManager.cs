using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance;

    [SerializeField] private List<BulletData> bulletDatas = new();

    private Dictionary<BulletType, GameObject> bulletPrefabDict;
    private Dictionary<BulletType, Stack<GameObject>> stackDict;

    private void Awake()
    {
        Instance = this;
        Initialization();
    }

    private void Initialization()
    {
        bulletPrefabDict = new Dictionary<BulletType, GameObject>(); // 딕셔너리 초기화
        stackDict = new Dictionary<BulletType, Stack<GameObject>>(); // 딕셔너리 초기화

        int bulletCount = bulletDatas.Count;

        for (int i = 0; i < bulletCount; i++)
        {
            BulletData bulletData = bulletDatas[i];
            bulletPrefabDict[bulletData.type] = bulletData.prefab;
            stackDict[bulletData.type] = new Stack<GameObject>(); // 수정된 부분

            for (int j = 0; j < bulletData.spawnSize; j++)
            {
                GameObject bullet = Instantiate(bulletData.prefab);
                bullet.SetActive(false);
                bullet.transform.parent = transform;
                stackDict[bulletData.type].Push(bullet); // 수정된 부분
            }
        }
    }

    private GameObject InstantiatePrefab(BulletType bulletType)
    {
        GameObject bullet = Instantiate(bulletPrefabDict[bulletType]);
        stackDict[bulletType].Push(bullet);

        return bullet;
    }

    public GameObject Spawn(BulletType bulletType, Vector2 spawnPoint, float angle)
    {
        GameObject bullet = null;

        if (stackDict[bulletType].Count > 0)
        {
            bullet = stackDict[bulletType].Pop();
        }
        else
        {
            bullet = InstantiatePrefab(bulletType);
        }
        
        bullet.SetActive(true);
        bullet.transform.position = spawnPoint;
        bullet.transform.parent = transform;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        return bullet;
    }

    public void DeSpawn(BulletType bulletType, GameObject bullet)
    {
        bullet.SetActive(false);
        stackDict[bulletType].Push(bullet);
    }
}
