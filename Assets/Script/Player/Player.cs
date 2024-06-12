using System.Collections;
using System.Xml;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public abstract class Player : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int curHp;

    [SerializeField] private Transform shootPositionLeft;
    [SerializeField] private Transform shootPositionRight;

    [SerializeField] private float shootDelayTime;

    [SerializeField] private Image unionWaitTimeGauge;
    private float unionTimer = 0;
    private const float unionDuration = 3.0f;
    private bool unionSet;
    private bool onStayPlayer;

    private BulletType bulletType;

    protected virtual void Start()
    {
        curHp = maxHp;
        bulletType = BulletType.Bullet;
        StartCoroutine(ShootDelay());
    }

    protected virtual void Update()
    {
        if(onStayPlayer)
        {
            if (unionTimer <= unionDuration && !unionSet)
            {
                unionTimer += Time.deltaTime;
                unionWaitTimeGauge.fillAmount = Mathf.Lerp(0, 1, unionTimer / unionDuration);

                if (unionTimer >= unionDuration)
                {
                    unionTimer = unionDuration;
                    SetAbleUnion();
                    unionSet = true;
                }
            }
        }
        else
        {
            unionSet = false;

            if(unionTimer > 0)
            {
                unionTimer -= Time.deltaTime * 5;
                unionWaitTimeGauge.fillAmount = Mathf.Lerp(0, 1, (unionTimer / unionDuration)); ;
            }
        }
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            HpDown();
        }
        
        if(collision.gameObject.CompareTag("Player"))
        {
            onStayPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onStayPlayer = false;
        }
    }

    protected virtual void SetAbleUnion()
    {

    }

    private IEnumerator ShootDelay()
    {
        ShootBullet();
        yield return new WaitForSeconds(shootDelayTime);
        StartCoroutine(ShootDelay());
    }

    private void ShootBullet()
    {
        BulletPoolManager.Instance.Spawn(bulletType, shootPositionLeft.position, -90);
        BulletPoolManager.Instance.Spawn(bulletType, shootPositionRight.position, -90);
    }

    private void HpDown()
    {
        curHp--;
        if (curHp <= 0)
        {
            GameOver();
        }
    }

    protected abstract void GameOver();
}
