// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class StageBackGroundManager : MonoBehaviour
{
    [SerializeField] private GameObject[] stageObject;

    public void SetStageBackGround(int stageIndex)
    {
        for (int i = 0; i < stageObject.Length; i++)
        {
            stageObject[i].SetActive(false);
        }

        stageObject[stageIndex].SetActive(true);
    }
}
