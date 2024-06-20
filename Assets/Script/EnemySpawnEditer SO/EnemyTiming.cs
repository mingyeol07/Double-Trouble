// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;
using UnityEngine.UI;

public class EnemyTiming : MonoBehaviour
{
    [SerializeField] private float curTime;
    [SerializeField] private float maxTime;

    [SerializeField] private Slider silde_TimeLine;

    private void Update()
    {
        if(curTime < maxTime) curTime += Time.deltaTime;
    }

    public float GetRealTime()
    {
        return curTime;
    }

    public float GetTimeValue()
    {
        return curTime / maxTime;
    }
}
