using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ü�� �÷��̾��� �����Ѿ��� ��� ����
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
