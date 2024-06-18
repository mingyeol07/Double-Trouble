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
        Instantiate(beam, shotTransform.position, Quaternion.identity);
    }

    private void Anim_ShotExit()
    {
        unionPlayer.weaponShoting = false;
    }
}
