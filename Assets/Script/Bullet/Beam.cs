using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] sprites;

    private void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        float time = 0.5f;

        while (time > 0)
        {
            time-= Time.deltaTime;
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].color = new Color(1, 1, 1, time);
            }
            yield return null;
        }
        Destroy(this.gameObject);
    }

    public int GetDamage()
    {
        return 1;
    }
}
