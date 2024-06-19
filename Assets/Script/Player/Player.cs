using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int curHp;

    [SerializeField] private List<Transform> shootPosition = new();

    [SerializeField] private float shootDelayTime;
    [SerializeField] private BulletType bulletType;

    private int level = 0;

    protected virtual void Start()
    {
        curHp = maxHp;
    }

    private void FixedUpdate()
    {
        MoveInput();
    }

    protected abstract void MoveInput();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("EnemyBeam"))
        {
            HpDown();
        }
    }

    private IEnumerator ShotDelay()
    {
        while(true)
        {
            ShotBullet();
            yield return new WaitForSeconds(shootDelayTime);
        }
    }

    protected virtual void ShotBullet()
    {
        for(int i = 0; i< shootPosition.Count; i++)
        {
            BulletPoolManager.Instance.Spawn(bulletType, shootPosition[i].position, 0);
        }
    }

    private void HpDown()
    {
        curHp--;
        if (curHp <= 0)
        {
            GameOver();
        }
    }

    protected virtual void GameOver()
    {

    }

    protected virtual void OnEnable()
    {
        StartCoroutine(ShotDelay());
    }

    private void LevelUp(Transform transform)
    {
        level++;
        shootPosition.Add(transform);
    }
}
