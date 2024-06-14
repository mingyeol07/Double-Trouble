using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private IEnumerator moveEnemy(Vector2 startPosition, Vector2 endPosition, float durationTime)
    {
        float time = 0;
        while (time < durationTime)
        {
            time += Time.deltaTime;
            transform.position = Vector2.Lerp(startPosition, endPosition, time/ durationTime);
            yield return null;
        }
    }
}
