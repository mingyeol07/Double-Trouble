using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 합체한 플레이어의 광선을 발사하는 엔진
/// </summary>
public class BeamEngine : MonoBehaviour
{
    [SerializeField] private GameObject beam;
    [SerializeField] private Transform shotTransform;
    [SerializeField] private UnionPlayer unionPlayer;

    private void ShotBeam()
    {
        GameObject go = Instantiate(beam);
        go.transform.position = shotTransform.position;
    }
}
