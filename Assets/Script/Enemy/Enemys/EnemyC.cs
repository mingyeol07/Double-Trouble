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
    [SerializeField] private GameObject beamWarning;

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

    protected override IEnumerator Co_StartMove(float moveTime, Vector2 startPosition, Vector2 endPosition)
    {
        isBeam = false;
        beamWarning.SetActive(false);
        return base.Co_StartMove(moveTime, startPosition, endPosition);
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
}
