using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ü�� �÷��̾��� ������ �߻��ϴ� ����
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
