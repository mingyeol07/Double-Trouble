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
    [SerializeField] private int curHp;

    [SerializeField] private float shootDelayTime;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float unionTime;
    [SerializeField] private GameObject unionLight;
    [SerializeField] private Animator engineAnim;
    private readonly int hashWeaponShot = Animator.StringToHash("Shot");
    private bool weaponShoting;
    private Rigidbody2D rigid;
    [SerializeField] private GameObject shield;
    [SerializeField] private Animator boostAnim;
    private readonly int hashBoosting = Animator.StringToHash("Boosting");

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        StartCoroutine(Shield());
        curHp = maxHp;
        GameObject go = Instantiate(unionLight, transform);
        go.transform.position = transform.position;
        Destroy(go, 1);
        PlayerManager.Instance.SetUnionPlayer(gameObject);
        StartCoroutine(Co_StartUnionTime());
        StartCoroutine(ShotDelay());
    }

    private IEnumerator Shield()
    {
        GameObject go = Instantiate(shield, transform);
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        Destroy(go);
        GetComponent<Collider2D>().enabled = true;
    }

    protected virtual void FixedUpdate()
    {
        MoveInput();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("EnemyBeam"))
        {
            HpDown();
        }
    }

    private IEnumerator ShotDelay()
    {
        while (true)
        {
            ShotBullet();
            yield return new WaitForSeconds(shootDelayTime);
        }
    }

    private void ShotBullet()
    {
        if (!weaponShoting)
        {
            weaponShoting = true;
            engineAnim.SetTrigger(hashWeaponShot);
            StartCoroutine(AnimExit());
        }
    }

    private IEnumerator AnimExit()
    {
        yield return new WaitForSeconds(0.01f);
        float time = engineAnim.GetCurrentAnimatorClipInfo(0).Length / 10;
        yield return new WaitForSeconds(time);
        weaponShoting = false;
    }

    private void HpDown()
    {
        Camera.main.GetComponent<CameraShake>().StartShake(0.3f);
        {
            if (curHp-- <= 0)
            GameOver();
        }
    }

    private void GameOver()
    {
        StopAllCoroutines();
        PlayerManager.Instance.ExitUnion();
        Destroy(this.gameObject);
    }

    private void MoveInput()
    {
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
