using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeSize;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = new Vector3(0, 0, -5f);
    }

    public void StartShake(float setShakeTime = 1f)
    {
        StartCoroutine(ShakeStart(setShakeTime));
    }

    private IEnumerator ShakeStart(float duration)
    {
        float timer = 0;
        while (timer <= duration)
        {
            transform.localPosition = Random.insideUnitSphere * shakeSize + initialPosition;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = initialPosition;
    }
}
