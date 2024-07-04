using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
/// <summary>
/// ÇÃ·¹ÀÌ¾î°¡ ½î´Â ºö
/// </summary>
public class PlayerBeam : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private BoxCollider2D coll;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = true;
        StartCoroutine(LateDestroy());
    }

    private IEnumerator LateDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        coll.enabled = false;
        float time = 1;
        while (time > 0)
        {
            time -= Time.deltaTime;
            spriteRenderer.color = new Color(1, 1, 1, time / 1);
            yield return null;
        }
    }
}
