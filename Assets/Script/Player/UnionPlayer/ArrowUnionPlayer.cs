using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowUnionPlayer : UnionPlayer
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform[] shotPosition;
    
    private void ShotArrow(int shotPositionIndex)
    {
        Instantiate(arrow, shotPosition[shotPositionIndex].position, Quaternion.identity);
    }
}
