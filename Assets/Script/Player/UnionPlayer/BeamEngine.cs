using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEngine : MonoBehaviour
{
    [SerializeField] private GameObject beam;
    [SerializeField] private Transform shotTransform;
    [SerializeField] private UnionPlayer unionPlayer;

    private void ShotBeam()
    {
        GameObject go = Instantiate(beam, shotTransform.position, Quaternion.identity);
    }
}
