using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Anim_ShotExit()
    {
        unionPlayer.weaponShoting = false;
    }
}
