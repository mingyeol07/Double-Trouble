// # Systems
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;


// # Unity
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyF : Enemy
{
    [SerializeField] private GameObject beam;
    [SerializeField] private GameObject beamWarning;

    protected override IEnumerator Co_StartMove(float moveTime, Vector2 startPosition, Vector2 endPosition)
    {
        beamWarning.SetActive(false);
        beam.SetActive(false);

        Vector2 direction = (startPosition - endPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        Vector2 velocity = Vector2.zero;
        float offset = 0.2f;

        transform.position = startPosition;

        while (Vector2.Distance((Vector2)transform.position, endPosition) > offset)
        {
            transform.position = Vector2.SmoothDamp(transform.position, endPosition, ref velocity, moveTime);
            yield return null;
        }

        Shoot();
    }

    protected override void Shoot()
    {
        StartCoroutine(Co_Beam());
    }

    private IEnumerator Co_Beam()
    {
        beamWarning.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        beamWarning.SetActive(false);

        yield return new WaitForSeconds(1f);

        beam.SetActive(true);

        yield return new WaitForSeconds(1f);

        beam.SetActive(false);
    }
}