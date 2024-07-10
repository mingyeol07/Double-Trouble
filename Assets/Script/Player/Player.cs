using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
/// <summary>
/// 플레이어 부모 클래스
/// </summary>
public abstract class Player : MonoBehaviour
{
    [SerializeField] protected GameObject[] miniPlayer;
    [SerializeField] private GameObject getItemLight;

    private const int maxHp = 3;
    protected int curHp;

    [SerializeField] private List<Transform> shootPosition = new();

    [SerializeField] private float shootDelayTime;
    [SerializeField] private BulletType bulletType;

    [SerializeField] private GameObject shield;

    private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private AudioClip itemPickUpSound;

    public bool isShield;

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        curHp = maxHp;
    }

    private void FixedUpdate()
    {
        if (transform.position.x >= 9)
        {
            transform.position = new Vector2(9, transform.position.y);
        }
        else if (transform.position.x <= -9)
        {
            transform.position = new Vector2(-9, transform.position.y);
        }

        if (transform.position.y >= 5)
        {
            transform.position = new Vector2(transform.position.x, 5);
        }
        else if (transform.position.y <= -5)
        {
            transform.position = new Vector2(transform.position.x, -5);
        }

        if (PlayerManager.Instance.isPlay) MoveInput();
    }


    protected abstract void MoveInput();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("EnemyBeam")) && !isShield)
        {
            Camera.main.GetComponent<CameraShake>().StartShake(0.3f);
            HpDown();
        }

        if(collision.gameObject.CompareTag("Item"))
        {
            audioSource.clip = itemPickUpSound;
            audioSource.Play();
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
        audioSource.clip = shootSound;
        audioSource.Play();

        for (int i = 0; i< shootPosition.Count; i++)
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

    public virtual void HpDown()
    {
        audioSource.clip = destroySound;
        audioSource.Play();
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
        shield.SetActive(true);
        isShield = true;
        yield return new WaitForSeconds(3f);
        isShield = false;
        shield.SetActive(false);
    }
}
