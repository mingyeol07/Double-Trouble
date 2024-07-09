using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EnemySpawnData;

[Serializable]
public class StageData
{
    public int maxTime;
    public List<EnemySpawnData> spawnDatas;

    public StageData()
    {
        spawnDatas = new List<EnemySpawnData>();
    }
}

public class EditorManager : MonoBehaviour
{
    [SerializeField] private EditorSetEnemyDropDown enemyDropDown;
    [SerializeField] private EditorSetStageDropDown stageDropDown;
    [SerializeField] private GameObject[] stageBG;
    private StageJsonSave jsonSave;

    [Header("GO")]
    [SerializeField] private List<GameObject> enemys;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject lineRendererObject;
    [SerializeField] private GameObject pnl_makeScreen;

    [Header("UI")]
    [SerializeField] private Button btn_deleteEvent;
    [SerializeField] private Button btn_making;
    [SerializeField] private Button btn_save;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text txt_slider_value;
    [SerializeField] private TMP_InputField inputField_maxTime;

    private string previousInput;
    [SerializeField] private Sprite[] enemySprite;

    [Header("VALUE")]
    [SerializeField] private int maxTime;
    [SerializeField] private int curTime;
    private int previousTime = 0;

    [Header("SAVE")]
    private StageData stageData;
    private EnemySpawnData enemySpawnData;

    private bool onClicked;
    private bool isMaking;

    private LineRenderer lineRendererClone;
    private GameObject enemyClone;

    private void Start()
    {
        SetUp();
        AddButtonListeners();
        StageChange(stageDropDown.stage);
        stageData.maxTime = maxTime;
    }

    private void Update()
    {
        HandleMakeInput();
        UpdateSlider();
        UpdateMaxTimeFromInput();
        ChangeTimeToInput();
    }

    private void ChangeTimeToInput()
    {
        float changeAmount = 1f / maxTime;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            slider.value = Mathf.Clamp(slider.value - changeAmount, 0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            slider.value = Mathf.Clamp(slider.value + changeAmount, 0, 1);
        }

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            slider.value += Time.deltaTime * Input.GetAxis("HorizontalArrow");
        }
    }

    private void SetUp()
    {
        jsonSave = GetComponent<StageJsonSave>();
        previousInput = inputField_maxTime.text;

        if (int.TryParse(previousInput, out int initialTime))
        {
            maxTime = initialTime;
        }

        for (int i = 0; i < 12; i++)
        {
            GameObject go = Instantiate(enemyPrefab);
            go.SetActive(false);
            enemys.Add(go);
        }
    }

    private void AddButtonListeners()
    {
        btn_making.onClick.AddListener(ToggleMakeMode);
        btn_save.onClick.AddListener(SaveData);
        btn_deleteEvent.onClick.AddListener(DeleteCurrentTimeEnemies);
    }

    private void LoadStageData()
    {
        stageData = new StageData { maxTime = maxTime };
        StageData loadedData = jsonSave.LoadData(stageDropDown.stage);

        if (loadedData != null)
        {
            foreach (var enemyData in loadedData.spawnDatas)
            {
                stageData.spawnDatas.Add(enemyData);
            }
        }

        UpdateTimelineScene();
    }

    private void SaveData()
    {
        if (!isMaking)
        {
            jsonSave.SaveData(stageData, stageDropDown.stage);
        }
    }

    public void StageChange(int stageIndex)
    {
        for (int i = 0; i < stageBG.Length; i++)
        {
            stageBG[i].SetActive(false);
        }

        stageBG[stageIndex].SetActive(true);
        LoadStageData();
    }

    private void UpdateMaxTimeFromInput()
    {
        string currentInput = inputField_maxTime.text;

        if (currentInput != previousInput && int.TryParse(currentInput, out int changeTime))
        {
            maxTime = changeTime;
            previousInput = currentInput;
            slider.maxValue = maxTime;
            UpdateSlider();
        }
    }

    private void UpdateSlider()
    {
        curTime = Mathf.RoundToInt(slider.value * maxTime);

        if (previousTime != curTime)
        {
            previousTime = curTime;
            UpdateTimelineScene();
        }

        txt_slider_value.text = curTime.ToString();
        slider.value = (float)curTime / maxTime;
    }

    private void UpdateTimelineScene()
    {
        foreach (var enemy in enemys)
        {
            enemy.SetActive(false);
        }

        foreach (var spawnData in stageData.spawnDatas)
        {
            if (spawnData.spawnTime == curTime)
            {
                GameObject enemy = GetAvailableEnemyPrefab();
                var spriteRenderer = enemy.GetComponent<SpriteRenderer>();
                var lineRenderer = enemy.GetComponent<LineRenderer>();

                spriteRenderer.sprite = enemySprite[(int)spawnData.enemyType];
                enemy.transform.position = new Vector2(spawnData.startPos.x, spawnData.startPos.y);
                lineRenderer.SetPosition(0, new Vector2(spawnData.startPos.x, spawnData.startPos.y));
                lineRenderer.SetPosition(1, new Vector2(spawnData.endPos.x, spawnData.endPos.y));
            }
        }
    }

    private GameObject GetAvailableEnemyPrefab()
    {
        foreach (var enemy in enemys)
        {
            if (!enemy.activeSelf)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }

        return null;
    }

    private void HandleMakeInput()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isMaking && !onClicked)
        {
            StartMake();
        }
        else if (Input.GetKeyDown(KeyCode.E) && isMaking && onClicked)
        {
            EndMake();
        }
    }

    private Vector2Int GetMousePosition()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2Int(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y));
    }

    private void StartMake()
    {
        onClicked = true;

        Vector2Int mousePos = GetMousePosition();
        CreateEnemyInstance(mousePos);
    }

    private void CreateEnemyInstance(Vector2Int spawnPos)
    {
        enemySpawnData = new EnemySpawnData
        {
            spawnTime = curTime,
            enemyType = enemyDropDown.enemyType,
            startPos = new IntVector2(spawnPos.x, spawnPos.y)
        };

        enemyClone = GetAvailableEnemyPrefab();
        lineRendererClone = enemyClone.GetComponent<LineRenderer>();

        enemyClone.transform.position = (Vector2)spawnPos;
        enemyClone.GetComponent<SpriteRenderer>().sprite = enemySprite[(int)enemyDropDown.enemyType];
        lineRendererClone.SetPosition(0, (Vector2)spawnPos);
        lineRendererClone.SetPosition(1, (Vector2)spawnPos);
    }

    private void EndMake()
    {
        onClicked = false;

        Vector2Int mousePos = GetMousePosition();
        SaveEnemyInstance(mousePos);
    }

    private void SaveEnemyInstance(Vector2Int endPos)
    {
        lineRendererClone.SetPosition(1, (Vector2)endPos);

        enemySpawnData.endPos = new IntVector2(endPos.x, endPos.y);
        stageData.spawnDatas.Add(enemySpawnData);

        enemySpawnData = null;
        ToggleMakeMode();
    }

    private void DeleteCurrentTimeEnemies()
    {
        foreach (var enemy in enemys)
        {
            enemy.SetActive(false);
        }

        stageData.spawnDatas.RemoveAll(spawnData => spawnData.spawnTime == curTime);
    }

    // 생성 모드 토글
    private void ToggleMakeMode()
    {
        isMaking = !isMaking;
        pnl_makeScreen.SetActive(isMaking);
    }
}
