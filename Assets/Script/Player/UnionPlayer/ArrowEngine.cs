using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 합체한 플레이어의 화살을 다량발사하는 엔진
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
