// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class StageBackGroundManager : MonoBehaviour
{
    [SerializeField] private GameObject[] stageObject;
    private AudioSource bgmAudio;

    [SerializeField] private bool isTesting;
    [SerializeField] private GameObject enemy;

    private void Awake()
    {
        bgmAudio = GetComponent<AudioSource>();

        if (isTesting)
        {
            GetComponent<EnemySpawnManager>().enabled = false;
            GameObject go = Instantiate(enemy);
            go.transform.position = new Vector3(0, 3, 0);
        }
    }

    private void Start()
    {
        bgmAudio.Play();
    }

    public void SetStageBackGround(int stageIndex)
    {
        for (int i = 0; i < stageObject.Length; i++)
        {
            stageObject[i].SetActive(false);
        }

        stageObject[stageIndex].SetActive(true);
    }
}
