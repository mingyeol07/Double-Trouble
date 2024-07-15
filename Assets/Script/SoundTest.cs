using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundTest : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    private void Start()
    {
        btn.onClick.AddListener(() => SoundCheck());
    }

    private void SoundCheck()
    {
        m_AudioSource.Play();
    }
}
