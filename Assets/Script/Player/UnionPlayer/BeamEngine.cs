using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 합체한 플레이어의 광선을 발사하는 엔진
/// </summary>
public class BeamEngine : MonoBehaviour
{
    [SerializeField] private GameObject[] miniPlayer;
    [SerializeField] private GameObject[] miniBeam;
    [SerializeField] private GameObject beam;
    private Collider2D beamColl;
    private UnionPlayer unionPlayer;

    private void Start()
    {
        unionPlayer = GetComponentInParent<UnionPlayer>();
        beamColl = beam.GetComponent<Collider2D>();
    }

    private void ShootBeam()
    {
        beam.SetActive(true);
        unionPlayer.ShootSound();
        for (int i = 0; i < miniPlayer.Length; i++)
        {
            if (miniPlayer[i].activeSelf)
            {
                miniBeam[i].SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        beam.SetActive(false) ;

        for (int i = 0; i < miniPlayer.Length; i++)
        {
            if (miniPlayer[i].activeSelf)
            {
                miniBeam[i].gameObject.SetActive(false);
            }
        }
    }
}
