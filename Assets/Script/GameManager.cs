using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button btn_title;
    [SerializeField] private Button btn_retry;
    [SerializeField] private Button btn_clear;

    private void Start()
    {
        btn_retry.onClick.AddListener(() => { SceneManager.LoadScene("Stage0"); });
        btn_title.onClick.AddListener(() => { SceneManager.LoadScene("Title"); });
        if (btn_clear != null) btn_clear.onClick.AddListener(() => { SceneManager.LoadScene("Title"); });
    }
}
