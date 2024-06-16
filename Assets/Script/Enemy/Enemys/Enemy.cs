using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType type;
    [SerializeField] private int maxHp;
    [SerializeField] private int curHp;
    [SerializeField] private float shootCoolTime;
    [SerializeField] private float moveDownSpeed;

    private SpriteRenderer spriteRenderer;
    private new BoxCollider2D collider;
    private Animator animator;
    private readonly int hashDestroy = Animator.StringToHash("Destroy");

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        curHp = maxHp;
       StartCoroutine(Co_Shot());
    }

    protected virtual void Update()
    {
        MoveDown();
    }

    private void MoveDown()
    {
        transform.Translate(Vector2.down.normalized * Time.deltaTime * moveDownSpeed, Space.World);
    }

    public void StartMove(float moveTime, Vector2 startPosition, Vector2 endPosition)
    {
        StartCoroutine(Co_StartMove(moveTime, startPosition, endPosition));
    }

    public void StartShoot()
    {
        StartCoroutine(Co_Shot());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HpDown(collision.gameObject.GetComponent<Bullet>().GetDamage());
        }
        else if (collision.gameObject.CompareTag("Beam"))
        {
            HpDown(collision.gameObject.GetComponent<Beam>().GetDamage());
        }
    }

    private void HpDown(int damage)
    {
        if (curHp <= 0)
            StartCoroutine(Co_Destroy());
        else
        {
            curHp -= damage;
            StartCoroutine(Co_OnHit());
        }
    }

    protected virtual IEnumerator Co_Destroy()
    {
        animator.SetTrigger(hashDestroy);
        collider.enabled = false;
        yield return new WaitForSeconds(1f);
        EnemyPoolManager.Instance.DeSpawn(type, this.gameObject);
    }

    /// <summary>
    /// Enemy가 스포될 때 실행시켜줘야하는 코루틴, targetTransform까지 moveTime동안 움직여서 포지션을 잡는 함수
    /// </summary>
    /// <param name="moveTime"></param>
    /// <param name="targetTransform"></param>
    /// <returns></returns>
    private IEnumerator Co_StartMove(float moveTime, Vector2 startPosition, Vector2 endPosition)
    {
        float time = 0;
        transform.position = startPosition;
        while (time < moveTime)
        {
            float t = time / moveTime;
            transform.position = Vector2.Lerp(startPosition, endPosition, t);
            time += Time.deltaTime;

            yield return null;
        }
        transform.position = endPosition;
    }

    protected abstract void Shot();

    private IEnumerator Co_Shot()
    {
        while (true)
        {
            Shot();
            yield return new WaitForSeconds(shootCoolTime);
        }
    }

    private IEnumerator Co_OnHit()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.1f);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void OnEnable()
    {
        curHp = maxHp;
        collider.enabled = true;
    }
}
