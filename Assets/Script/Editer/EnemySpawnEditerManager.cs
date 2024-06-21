// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnEditerManager : MonoBehaviour
{
    [SerializeField] private EnemyWayPointData[] waypoints;

    [SerializeField] private EnemyTiming timing;
    [SerializeField] private Toggle stopToggle;
    [SerializeField] private Button createPattern;

    private void SaveJsonData()
    {

    }
}
