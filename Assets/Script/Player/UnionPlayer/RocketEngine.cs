using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 합체한 플레이어의 로켓을 발사하는 엔진
/// </summary>
public class RocketEngine : MonoBehaviour
{
    [SerializeField] private Transform shotLeftTransform;
    [SerializeField] private Transform shotRightTransform;
    [SerializeField] private UnionPlayer unionPlayer;

    private void ShotLeftRocket()
    {
        BulletPoolManager.Instance.Spawn(BulletType.Rocket, shotLeftTransform.position, 0);
    }

    private void ShotRightRocket()
    {
        BulletPoolManager.Instance.Spawn(BulletType.Rocket, shotRightTransform.position, 0);
    }
}
