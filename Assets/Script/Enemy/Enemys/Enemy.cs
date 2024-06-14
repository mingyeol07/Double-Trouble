using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType type;
    [SerializeField] private int maxHp;
    [SerializeField] private int curHp;
    [SerializeField] private float shootCoolTime;
    [SerializeField] protected Transform shootTransform;

    private void Start()
    {
        curHp = maxHp;
        StartCoroutine(Co_Shoot());
    }

    public void StartMove(float moveTime, Vector2 startPosition, Vector2 endPosition)
    {
        StartCoroutine(Co_StartMove(moveTime, startPosition, endPosition));
    }

    public void StartShoot()
    {
        StartCoroutine(Co_Shoot());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HpDown(collision.gameObject.GetComponent<Bullet>().GetDamage());
        }
    }

    private void HpDown(int damage)
    {
        curHp -= damage;
        if (curHp <= 0) Destroy();
    }
    
    private void Destroy()
    {
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

    protected abstract void Shoot();

    private IEnumerator Co_Shoot()
    {
        Shoot();
        yield return new WaitForSeconds(shootCoolTime);
        StartCoroutine(Co_Shoot());
    }

    private void OnEnable()
    {
        curHp = maxHp;
    }
}
