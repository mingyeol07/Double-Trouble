using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
/// <summary>
/// 합체한 플레이어
/// </summary>
public class UnionPlayer : MonoBehaviour
{
    [SerializeField] private int maxHp;

    [SerializeField] protected GameObject[] miniPlayer;
    [SerializeField] private GameObject getItemLight;

    [SerializeField] private float shootDelayTime;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float unionTime;
    [SerializeField] private GameObject unionLight;
    [SerializeField] private GameObject shield;
    [SerializeField] private Animator engineAnim;
    [SerializeField] private Animator boostAnim;

    private bool weaponShooting;
    private bool isSheild;
    private Rigidbody2D rigid;
    [SerializeField] private AudioClip lightSound;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private AudioClip itemPickUpSound;
    private AudioSource audioSource;

    private readonly int hashWeaponShoot = Animator.StringToHash("Shoot");
    private readonly int hashBoosting = Animator.StringToHash("Boosting");

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        PlayerManager.Instance.SetUnionPlayer(gameObject);

        OnLight();
        StartCoroutine(Co_StartUnionTime());
        StartCoroutine(ShootDelay());
        StartCoroutine(Shield());
    }

    private void OnLight()
    {
        audioSource.clip = lightSound;
        audioSource.Play();

        GameObject go = Instantiate(unionLight, transform);
        go.transform.position = transform.position;
        Destroy(go, 1);
    }

    public void ShootSound()
    {
        audioSource.clip = shootSound;
        audioSource.Play();
    }

    private IEnumerator Shield()
    {
        shield.SetActive(true); 
        isSheild = true;
        yield return new WaitForSeconds(3f);
        isSheild = false;
        shield.SetActive(false);
    }

    protected virtual void FixedUpdate()
    {
        MoveInput();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("EnemyBeam"))&& !isSheild)
        {
            ExitUnion();
        }

        if (collision.gameObject.CompareTag("Item"))
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
        while (true)
        {
            ShootBullet();
            yield return new WaitForSeconds(shootDelayTime);
        }
    }

    private void ShootBullet()
    {
        if (!weaponShooting)
        {
            weaponShooting = true;
            engineAnim.SetTrigger(hashWeaponShoot);
            StartCoroutine(AnimExit());
        }
    }

    private IEnumerator AnimExit()
    {
        yield return new WaitForSeconds(0.01f);
        float time = engineAnim.GetCurrentAnimatorClipInfo(0).Length / 10;
        yield return new WaitForSeconds(time);
        weaponShooting = false;
    }

    private void ExitUnion()
    {
        audioSource.clip = destroySound;
        audioSource.Play();

        Camera.main.GetComponent<CameraShake>().StartShake(0.3f);
        StopAllCoroutines();
        PlayerManager.Instance.ExitUnion();
        Destroy(this.gameObject);
    }

    private void MoveInput()
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

        float h = Input.GetAxisRaw("HorizontalMultiple");
        float v = Input.GetAxisRaw("VerticalMultiple");

        rigid.velocity = new Vector2(h, v).normalized * moveSpeed;

        boostAnim.SetBool(hashBoosting, v != 0 || h != 0);
    }

    private IEnumerator Co_StartUnionTime()
    {
        yield return new WaitForSeconds(unionTime);
        PlayerManager.Instance.ExitUnion();
        Destroy(this.gameObject);
    }
}
