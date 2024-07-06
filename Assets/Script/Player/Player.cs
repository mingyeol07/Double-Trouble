using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEditor.Rendering;
using UnityEngine;
/// <summary>
/// 플레이어 부모 클래스
/// </summary>
public abstract class Player : MonoBehaviour
{
    [SerializeField] protected GameObject[] miniPlayer;
    [SerializeField] private GameObject getItemLight;

    [SerializeField] private int maxHp;
    [SerializeField] private int curHp;

    [SerializeField] private List<Transform> shootPosition = new();

    [SerializeField] private float shootDelayTime;
    [SerializeField] private BulletType bulletType;

    [SerializeField] private GameObject shield;

    protected virtual void Start()
    {
        curHp = maxHp;
    }

    private void FixedUpdate()
    {
       if(PlayerManager.Instance.isPlay) MoveInput();
    }

    protected abstract void MoveInput();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("EnemyBeam"))
        {
            Camera.main.GetComponent<CameraShake>().StartShake(0.3f);
            HpDown();
        }

        if(collision.gameObject.CompareTag("Item"))
        {
            Destroy(Instantiate(getItemLight, transform.position, Quaternion.identity), 1f);
            Destroy(collision.gameObject);
            SpawnMiniPlayer();
        }
    }

    protected virtual void SpawnMiniPlayer()
    {
        for (int i = 0; i < miniPlayer.Length; i++)
        {
            miniPlayer[i].SetActive(true);
            miniPlayer[i].GetComponent<PlayerMini>().SetTime();
        }
    }

    private IEnumerator ShootDelay()
    {
        while(true)
        {
            ShootBullet();
            yield return new WaitForSeconds(shootDelayTime);
        }
    }

    protected virtual void ShootBullet()
    {
        for(int i = 0; i< shootPosition.Count; i++)
        {
            BulletPoolManager.Instance.Spawn(bulletType, shootPosition[i].position, 0);
        }

        for(int i = 0; i < miniPlayer.Length; i++)
        {
            if(miniPlayer[i].activeSelf)
            {
                miniPlayer[i].GetComponent<PlayerMini>().Shoot();
            }
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
        if(PlayerManager.Instance.isPlay == true) StartSetup();
    }

    public void StartSetup()
    {
        StartCoroutine(ShootDelay());
        StartCoroutine(Shield());
    }

    private IEnumerator Shield()
    {
        GameObject go = Instantiate(shield, transform);
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        GetComponent<Collider2D>().enabled = true;
        Destroy(go);
    }
}
