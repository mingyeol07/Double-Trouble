using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
/// <summary>
/// ÇÃ·¹ÀÌ¾î°¡ ½î´Â ºö
/// </summary>
public class PlayerBeam : MonoBehaviour
{
    private BoxCollider2D coll;
    private bool isCoroutine;

    void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        isCoroutine = true;
        StartCoroutine(DelayColliderOnOff());
    }

    private IEnumerator DelayColliderOnOff()
    {
        while (isCoroutine)
        {
            coll.enabled = !coll.enabled;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnDisable()
    {
        isCoroutine = false;
    }
}
