using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance;

    [SerializeField] private List<BulletData> bulletDatas = new();

    private Dictionary<BulletType, GameObject> bulletPrefabDict;
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
        bulletPrefabDict = new Dictionary<BulletType, GameObject>(); // 딕셔너리 초기화
        stackDict = new Dictionary<GameObject, Stack<GameObject>>(); // 딕셔너리 초기화

        int bulletCount = bulletDatas.Count;

        for (int i = 0; i < bulletCount; i++)
        {
            BulletData bulletData = bulletDatas[i];
            bulletPrefabDict[bulletData.type] = bulletData.prefab;
            stackDict[bulletData.prefab] = new Stack<GameObject>(); // 수정된 부분

            for (int j = 0; j < 200; j++)
            {
                GameObject bullet = Instantiate(bulletData.prefab);
                bullet.SetActive(false);
                bullet.transform.parent = transform;
                stackDict[bulletData.prefab].Push(bullet); // 수정된 부분
            }
        }
    }

    private GameObject InstantiatePrefab(BulletType bulletType)
    {
        GameObject bullet = Instantiate(bulletPrefabDict[bulletType]);
        stackDict[bulletPrefabDict[bulletType]].Push(bullet);
        bullet.transform.parent = transform;
        return bullet;
    }

    public GameObject Spawn(BulletType bulletType, Transform parent)
    {
        GameObject bullet = null;

        if (stackDict[bulletPrefabDict[bulletType]].Count > 0)
        {
            bullet = stackDict[bulletPrefabDict[bulletType]].Pop();
        }
        else
        {
            bullet = InstantiatePrefab(bulletType);
        }

        bullet.SetActive(true);
        //bullet.transform.parent = parent;

        return bullet;
    }

    public void DeSpawn(BulletType bulletType, GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.parent = transform;
        stackDict[bulletPrefabDict[bulletType]].Push(bullet);
    }
}
