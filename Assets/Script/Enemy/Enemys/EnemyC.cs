// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class EnemyC : Enemy
{
    private float angle;
    private readonly float rotationSpeed = 6f;
    private bool isBeam;
    [SerializeField] private GameObject beam;
    [SerializeField] private SpriteRenderer[] beamSpriteRenderer;
    [SerializeField] private Collider2D beamCollider;

    protected override void Update()
    {
        base.Update();
        if (!isBeam)
        {
            LookAtPlayer();
        }
    }

    private void LookAtPlayer()
    {
        angle = PlayerManager.Instance.GetLookNearPlayerAngle(transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90), Time.deltaTime * rotationSpeed);
    }

    protected override void Shot()
    {
        beam.SetActive(true);
        StartCoroutine(BeamAnim());
    }

    private IEnumerator BeamAnim()
    {
        // �����ϴ½ð� 2
        for (int i = 0; i < beamSpriteRenderer.Length; i++)
        {
            beamSpriteRenderer[i].color = new Color(1, 1, 1, 0.1f);
        }
        yield return new WaitForSeconds(2);

        // ������ ���ϰ� ��ٸ� 1
        isBeam = true;
        yield return new WaitForSeconds(1);

        // �߻� 
        beamCollider.enabled = true;
        for (int i = 0; i < beamSpriteRenderer.Length; i++)
        {
            beamSpriteRenderer[i].color = Color.white;
        }
        // ���� �����ִ� �ð� 1
        yield return new WaitForSeconds(1);
        beamCollider.enabled = false;

        // ����� 1
        float beamChargeTime = 1;
        float time = 1;
        while (time > 0)
        {
            time -= Time.deltaTime;
            for (int i = 0; i < beamSpriteRenderer.Length; i++)
            {
                beamSpriteRenderer[i].color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, time / beamChargeTime);
            }
            yield return null;
        }
        isBeam = false;
        beam.SetActive(false);
    }
}
