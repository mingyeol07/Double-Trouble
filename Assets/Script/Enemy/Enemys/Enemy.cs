using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
/// <summary>
/// Enemy���� �θ�Ŭ����
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

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        curHp = maxHp;
    }

    protected virtual void Update()
    {
        MoveDown();
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
            HpDown(100);
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
        animator.SetTrigger(hashDestroy);
        collider.enabled = false;
        yield return new WaitForSeconds(0.7f);
        EnemyPoolManager.Instance.DeSpawn(type, this.gameObject);
    }

    /// <summary>
    /// Enemy�� ������ �� �����������ϴ� �ڷ�ƾ, targetTransform���� moveTime���� �������� �������� ��� �Լ�
    /// </summary>
    /// <param name="moveTime"></param>
    /// <param name="targetTransform"></param>
    /// <returns></returns>
    protected virtual IEnumerator Co_StartMove(float moveTime, Vector2 startPosition, Vector2 endPosition)
    {
        Vector2 velocity = Vector2.zero;
        float offset = 0.1f;

        transform.position = startPosition;

        while (Vector2.Distance((Vector2)transform.position, endPosition) > offset)
        {
            transform.position = Vector2.SmoothDamp(transform.position, endPosition, ref velocity, moveTime);
            yield return null;
        }

        transform.position = endPosition;
        StartShoot();
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
