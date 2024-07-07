using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ü�� �÷��̾��� ȭ���� �ٷ��߻��ϴ� ����
/// </summary>
public class ArrowEngine : MonoBehaviour
{
    [SerializeField] private GameObject[] miniPlayer;
    [SerializeField] private Transform[] shotPosition;
    private UnionPlayer unionPlayer;

    private void Start()
    {
        unionPlayer = GetComponentInParent<UnionPlayer>();
        for (int i = 0; i < miniPlayer.Length; i++)
        {
            miniPlayer[i].GetComponent<PlayerMini>().SetBulletType(BulletType.Arrow);
        }
    }

    private void ShootArrow(int shootPositionIndex)
    {
        unionPlayer.ShootSound();

        BulletPoolManager.Instance.Spawn(BulletType.Arrow, shotPosition[shootPositionIndex].position, 0);

       if(shootPositionIndex == 2 || shootPositionIndex == 5)
        {
            for(int i = 0; i < miniPlayer.Length; i++)
            {
                if (miniPlayer[i].activeSelf) {
                    miniPlayer[i].GetComponent<PlayerMini>().Shoot();
                }
            }
        }
    }
}
