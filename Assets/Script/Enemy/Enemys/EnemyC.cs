// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;
/// <summary>
/// Beam�� ��� Enemy
/// </summary>
public class EnemyC : Enemy
{
    private float angle;
    private readonly float rotationSpeed = 6f;
    private bool isBeam;
    [SerializeField] private GameObject beam;
    [SerializeField] private GameObject beamWarning;

    protected override void Update()
    {
        base.Update();
        if (!isBeam)
        {
            LookAtPlayer();
        }
    }
    //�÷��̾ �ٶ�
    private void LookAtPlayer()
    {
        angle = PlayerManager.Instance.GetLookNearPlayerAngle(transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90), Time.deltaTime * rotationSpeed);
    }

    protected override void Shoot()
    {
        StartCoroutine(ShootBeam());
    }

    private IEnumerator ShootBeam()
    {
        isBeam = true;
        beamWarning.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        beamWarning.SetActive(false);

        yield return new WaitForSeconds(1f);

        beam.SetActive(true);

        yield return new WaitForSeconds(1f);

        isBeam = false;
    }

    private IEnumerator BeamWarning()
    {
        yield return null;
    }
}
