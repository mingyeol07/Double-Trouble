using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMini : MonoBehaviour
{
    [SerializeField] private bool isLeft;
    [SerializeField] private Transform shootTransform;
    [SerializeField] private float time;
    private BulletType bulletType;

    private void Awake()
    {
        if(isLeft)
        {
            bulletType = BulletType.LeftBullet;
        }
        else
        {
            bulletType = BulletType.RightBullet;
        }
    }

    private void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
        }

        if(time <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Shoot()
    {
        BulletPoolManager.Instance.Spawn(bulletType, shootTransform.position, 0);
    }

    private void OnEnable()
    {
        SetTime();
    }

    public void SetTime()
    {
        time = 10;
    }

    public void SetBulletType(BulletType type)
    {
        bulletType = type;
    }
}
