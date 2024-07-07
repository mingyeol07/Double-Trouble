using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button btn_title;
    [SerializeField] private Button btn_retry;

    private void Start()
    {
        btn_retry.onClick.AddListener(() => { SceneManager.LoadScene(1); });
        btn_title.onClick.AddListener(() => { SceneManager.LoadScene(0); });
    }
}
