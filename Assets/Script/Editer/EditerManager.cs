using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SaveData
{
    public float spawnTime;
    public List<EditerData> editerDatas = new List<EditerData>();
}

public class EditerManager : MonoBehaviour
{
    //[SerializeField] private Button btn_minusTime;
    //[SerializeField] private Button btn_plusTime;

    [SerializeField] private EditerSetEnemyDropDown enemyDropDown;

    [SerializeField] private GameObject lineRendererObject;
    [SerializeField] private GameObject pnl_makeScreen;
    private LineRenderer lineRenderer;

    [SerializeField] private Button btn_deleteEvent;
    [SerializeField] private Button btn_making;

    [SerializeField] private TMP_Dropdown drop_enemyType;

    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text slider_value;

    private bool isMaking;

    [SerializeField] private Dictionary<float, EditerData> editerDict;

    [SerializeField] private float maxTime;
    [SerializeField] private float curTime;

    private void Start()
    {
        btn_making.onClick.AddListener(() => MakeModeOnOff());
    }

    private void Update()
    {
        MakeInput();

        slider_value.text = Mathf.FloorToInt(slider.value * maxTime).ToString();
        curTime = Mathf.FloorToInt(slider.value * maxTime);
    }

    private void MakeInput()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            StartMake();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            EndMake();
        }
    }

    private void StartMake()
    {

    }

    private void EndMake()
    {

    }

    private void MakeModeOnOff()
    {
        isMaking = !isMaking;
        pnl_makeScreen.SetActive(isMaking);
    }
}
