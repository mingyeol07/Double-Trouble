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
            for(int y = -8; y < 9; y++)
            {
                Instantiate(positionPoint, new Vector2(x, y), Quaternion.identity);
            }
        }
    }
}
