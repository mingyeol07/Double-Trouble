using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
/// <summary>
/// Enemy들의 부모클래스
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType type;
    [SerializeField] private int maxHp;
    [SerializeField] private int curHp;
    [SerializeField] private float shootCoolTime;
    [SerializeField] private float moveDownSpeed;

    private SpriteRenderer spriteRenderer;
    private new Collider2D collider;
    private Animator animator;
    private readonly int hashDestroy = Animator.StringToHash("Destroy");
    [SerializeField] private Sprite defaultSprite;
    private bool onMoveDown;

    private AudioSource audioSource;
    [SerializeField] private AudioClip destorySound;
    [SerializeField] private AudioClip shootSound;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        if(onMoveDown) MoveDown();
    }

    private void MoveDown()
    {
        transform.Translate(Vector2.down * Time.deltaTime * moveDownSpeed, Space.World);
    }

    public virtual void StartMove(float moveTime, Vector2 startPosition, Vector2 endPosition)
    {
        StartCoroutine(Co_StartMove(moveTime, startPosition, endPosition));
    }

    public void StartShoot()
    {
        if(gameObject.activeSelf) StartCoroutine(Co_Shot());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HpDown(collision.gameObject.GetComponent<Bullet>().GetDamage());
        }
        else if (collision.gameObject.CompareTag("Beam"))
        {
            HpDown(2);
        }

        if(collision.gameObject.CompareTag("EnemyDestroy"))
        {
            StartCoroutine(Co_Destroy());
        }
    }

    private void HpDown(int damage)
    {
        if ((curHp - damage) <= 0)
            StartCoroutine(Co_Destroy());
        else
        {
            curHp -= damage;
            StartCoroutine(Co_OnHit());
        }
    }

    protected virtual IEnumerator Co_Destroy()
    {
        audioSource.clip = destorySound;
        audioSource.Play();
        animator.SetTrigger(hashDestroy);
        collider.enabled = false;
        onMoveDown = false;
        DropItem();
        yield return null;  
    }

    private void DestroyAnimatorEvent()
    {
        spriteRenderer.sprite = defaultSprite;
        EnemyPoolManager.Instance.DeSpawn(type, this.gameObject);
    }

    private void DropItem()
    {
        int random = Random.Range(0, 5);
        if(random == 0)
        {
            Instantiate(BulletPoolManager.Instance.item, transform.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// Enemy가 스폰될 때 실행시켜줘야하는 코루틴, targetTransform까지 moveTime동안 움직여서 포지션을 잡는 함수
    /// </summary>
    /// <param name="moveTime"></param>
    /// <param name="targetTransform"></param>
    /// <returns></returns>
    protected virtual IEnumerator Co_StartMove(float moveTime, Vector2 startPosition, Vector2 endPosition)
    {
        Vector2 velocity = Vector2.zero;
        float offset = 0.2f;

        transform.position = startPosition;

        while (Vector2.Distance((Vector2)transform.position, endPosition) > offset)
        {
            transform.position = Vector2.SmoothDamp(transform.position, endPosition, ref velocity, moveTime);
            yield return null;
        }

        StartShoot();
    }
        
    protected abstract void Shoot();

    protected IEnumerator Co_Shot()
    {
        onMoveDown = true;
        while (true)
        {
            if (!PlayerManager.Instance.isPlay) break;

            audioSource.clip = shootSound;
            audioSource.Play();
            Shoot();
            yield return new WaitForSeconds(shootCoolTime);
        }
    }

    private IEnumerator Co_OnHit()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void OnEnable()
    {
        curHp = maxHp;
        collider.enabled = true;
    }
}
