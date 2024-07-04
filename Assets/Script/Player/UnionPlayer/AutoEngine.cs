using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 합체한 플레이어의 오토총알을 쏘는 엔진
/// </summary>
public class AutoEngine : MonoBehaviour
{
    [SerializeField] private Transform shotLeftTransform;
    [SerializeField] private Transform shotRightTransform;
    [SerializeField] private UnionPlayer unionPlayer;

    private void ShotAuto()
    {
        BulletPoolManager.Instance.Spawn(BulletType.Auto, shotLeftTransform.position, 0);
        BulletPoolManager.Instance.Spawn(BulletType.Auto, shotRightTransform.position, 0);
    }
}
