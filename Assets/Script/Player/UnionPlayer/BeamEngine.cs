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
        go.transform.parent = transform;
    }

    private void Anim_ShotExit()
    {
        unionPlayer.weaponShoting = false;
    }
}
