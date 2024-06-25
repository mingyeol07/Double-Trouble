using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditerManager : MonoBehaviour
{
    //[SerializeField] private Button btn_minusTime;
    //[SerializeField] private Button btn_plusTime;

    [SerializeField] private GameObject lineRendererObject;
    [SerializeField] private GameObject makeScreen;
    private LineRenderer lineRenderer;

    [SerializeField] private Button btn_deleteEvent;
    [SerializeField] private Toggle tgl_making;

    [SerializeField] private TMP_Dropdown drop_enemyType;

    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text slider_value;

    private bool isStartMake;

    [SerializeField] private float maxTime;
    [SerializeField] private float curTime;

    private void Update()
    {
        slider_value.text = Mathf.FloorToInt(slider.value * maxTime).ToString();
        curTime = Mathf.FloorToInt(slider.value * maxTime);
    }
}
