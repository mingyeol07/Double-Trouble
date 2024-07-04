// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;
/// <summary>
/// Beam을 쏘는 Enemy
/// </summary>
public class EnemyC : Enemy
{
    private float angle;
    private readonly float rotationSpeed = 6f;
    private bool isBeam;
    [SerializeField] private GameObject beam;

    protected override void Update()
    {
        base.Update();
        if (!isBeam)
        {
            LookAtPlayer();
        }
    }
    //플레이어를 바라봄
    private void LookAtPlayer()
    {
        angle = PlayerManager.Instance.GetLookNearPlayerAngle(transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90), Time.deltaTime * rotationSpeed);
    }

    protected override void Shot()
    {
        beam.SetActive(true);
        StartCoroutine(Co_BeamAnim());
    }

    private IEnumerator Co_BeamAnim()
    {
        // 조준하는시간 
        yield return new WaitForSeconds(2);

        isBeam = true;
        // 방향을 정하고 기다림 
        yield return new WaitForSeconds(2);
        isBeam = false;
    }
}
