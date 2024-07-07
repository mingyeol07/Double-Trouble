using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ü�� �÷��̾��� ������ �߻��ϴ� ����
/// </summary>
public class RocketEngine : MonoBehaviour
{
    [SerializeField] private GameObject[] miniPlayer;
    [SerializeField] private Transform shotLeftTransform;
    [SerializeField] private Transform shotRightTransform;

    private void Start()
    {
        for (int i = 0; i < miniPlayer.Length; i++)
        {
            miniPlayer[i].GetComponent<PlayerMini>().SetBulletType(BulletType.Rocket);
        }
    }

    private void ShootLeftRocket()
    {
        BulletPoolManager.Instance.Spawn(BulletType.Rocket, shotLeftTransform.position, 0);
    }

    private void ShootRightRocket()
    {
        BulletPoolManager.Instance.Spawn(BulletType.Rocket, shotRightTransform.position, 0);

        for (int i = 0; i < miniPlayer.Length; i++)
        {
            if (miniPlayer[i].activeSelf)
            {
                miniPlayer[i].GetComponent<PlayerMini>().Shoot();
            }
        }
    }
}
