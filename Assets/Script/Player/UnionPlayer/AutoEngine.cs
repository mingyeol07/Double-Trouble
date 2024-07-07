using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ü�� �÷��̾��� �����Ѿ��� ��� ����
/// </summary>
public class AutoEngine : MonoBehaviour
{
    [SerializeField] private GameObject[] miniPlayer;
    [SerializeField] private Transform shotLeftTransform;
    [SerializeField] private Transform shotRightTransform;
    private UnionPlayer unionPlayer;

    private void Start()
    {
        unionPlayer = GetComponentInParent<UnionPlayer>();
        for (int i = 0; i < miniPlayer.Length; i++)
        {
            miniPlayer[i].GetComponent<PlayerMini>().SetBulletType(BulletType.Auto);
        }
    }

    private void ShootAuto()
    {
        unionPlayer.ShootSound();

        BulletPoolManager.Instance.Spawn(BulletType.Auto, shotLeftTransform.position, 0);
        BulletPoolManager.Instance.Spawn(BulletType.Auto, shotRightTransform.position, 0);

        for (int i = 0; i < miniPlayer.Length; i++)
        {
            if (miniPlayer[i].activeSelf)
            {
                miniPlayer[i].GetComponent<PlayerMini>().Shoot();
            }
        }
    }
}
