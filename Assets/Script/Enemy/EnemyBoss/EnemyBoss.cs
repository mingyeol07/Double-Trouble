// # Systems
using System.Collections;

// # Unity
using UnityEngine;

public class EnemyBoss : Enemy
{
    [SerializeField] private BossBarrier barrierLeft;
    [SerializeField] private BossBarrier barrierRight;

    protected override void Shot()
    {
        int ranNum = 0; // Random.Range(0, 5);

        switch(ranNum)
        {
            case 0:
                CirclePattern();
                break;
            case 1:
                BarrierPattern();
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

    private void CirclePattern()
    {
        for (int i = 0; i < 360; i += 13)
        {
            BulletPoolManager.Instance.Spawn(BulletType.EnemyBullet, transform.position, i).GetComponent<Bullet>().SetSpeed(3);
        }
    }

    private void BarrierPattern()
    {
        barrierLeft.gameObject.SetActive(true);
        barrierRight.gameObject.SetActive(true);

        if(barrierLeft.GetOnLeftBarriering() && barrierRight.GetOnRightRarriering())
        {

        }
    }

    protected override IEnumerator Co_Destroy()
    {
        yield return null;
    }
}
