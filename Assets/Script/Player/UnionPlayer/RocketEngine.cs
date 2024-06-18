using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEngine : MonoBehaviour
{
    [SerializeField] private Transform shotLeftTransform;
    [SerializeField] private Transform shotRightTransform;
    [SerializeField] private UnionPlayer unionPlayer;

    private void shotLeftMagnet()
    {
        BulletPoolManager.Instance.Spawn(BulletType.Magnet, shotLeftTransform.position, 0);
    }

    private void shotRightMagnet()
    {
        BulletPoolManager.Instance.Spawn(BulletType.Magnet, shotRightTransform.position, 0);
    }

    private void Anim_ShotExit()
    {
        unionPlayer.weaponShoting = false;
    }
}
