using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ü�� �÷��̾��� ȭ���� �ٷ��߻��ϴ� ����
/// </summary>
public class ArrowEngine : MonoBehaviour
{
    [SerializeField] private Transform[] shotPosition;
    [SerializeField] private UnionPlayer unionPlayer;
    
    private void ShotArrow(int shotPositionIndex)
    {
        BulletPoolManager.Instance.Spawn(BulletType.Arrow, shotPosition[shotPositionIndex].position, 0);
    }
}
