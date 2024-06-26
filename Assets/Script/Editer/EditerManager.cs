using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class StageData
{
    private Dictionary<float, List<EnemySpawnData>> spawnDatas;
}

public class EditerManager : MonoBehaviour
{
    //[SerializeField] private Button btn_minusTime;
    //[SerializeField] private Button btn_plusTime;

    [SerializeField] private EditerSetEnemyDropDown enemyDropDown;

    [Header("GO")]
    [SerializeField] private GameObject lineRendererObject;
    [SerializeField] private GameObject pnl_makeScreen;
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;

    [Header("UI")]
    [SerializeField] private Button btn_deleteEvent;
    [SerializeField] private Button btn_making;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text slider_value;
    [SerializeField] private TMP_Dropdown drop_enemyType;

    [Header("VALUE")]
    [SerializeField] private float maxTime;
    [SerializeField] private float curTime;

    private StageData stageData;
    private bool onClicked;
    private bool isMaking;
    private LineRenderer lineRenderer;

    private void Start()
    {
        btn_making.onClick.AddListener(() => MakeModeOnOff());
    }

    private void Update()
    {
        MakeInput();

        slider_value.text = "Time : " + Mathf.FloorToInt(slider.value * maxTime).ToString();
        curTime = Mathf.FloorToInt(slider.value * maxTime);
    }

    private void MakeInput()
    {
        if(Input.GetKeyDown(KeyCode.Q) && isMaking)
        {
            if (!onClicked) StartMake();
        }
        else if (Input.GetKeyDown(KeyCode.E) && isMaking)
        {
            if(onClicked) EndMake();
        }
    }

    private void StartMake()
    {
        onClicked = true;
        GameObject point = Instantiate(startPoint);
        point.transform.position = new Vector2( Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    }

    private void EndMake()
    {
        onClicked = false;
        MakeModeOnOff();
        GameObject point = Instantiate(endPoint);
        point.transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    }

    private void MakeModeOnOff()
    {
        isMaking = !isMaking;

        pnl_makeScreen.SetActive(isMaking);
    }
}
