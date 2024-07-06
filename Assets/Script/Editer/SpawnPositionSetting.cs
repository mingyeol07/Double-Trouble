// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class SpawnPositionSetting : MonoBehaviour
{
    [SerializeField] private GameObject positionPoint;

    private void Start()
    {
        for(int x = -13; x < 14; x ++)
        {
            for (int y = -8; y < 12; y++)
            {
                GameObject point = Instantiate(positionPoint);
                point.transform.position = new Vector2(x, y);
            }
        }
    }
}
