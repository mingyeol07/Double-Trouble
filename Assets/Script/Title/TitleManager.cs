using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public static TitleManager Instance;

    [SerializeField] private Animator canvasAnimator;
    private readonly int hashSetting = Animator.StringToHash("OnSetting");
    private readonly int hashHowToPlay = Animator.StringToHash("OnHowToPlay");
    private readonly int hashCredit = Animator.StringToHash("OnCredit");

    private void Awake()
    {
        Instance = this;
    }

    public void ButtonEvent(TitleButtonType type, bool active)
    {
        switch (type)
        {
            case TitleButtonType.Exit:
                if (active) Exit();
                break;
            case TitleButtonType.Start:
                if (active) StartGame(); 
                break;
            case TitleButtonType.Setting:
                Setting(active);
                break;
            case TitleButtonType.HowToPlay:
                HowToPlay(active);
                break;
            case TitleButtonType.Credit: 
                Credit(active);
                break;
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("InGame");
    }
    private void Exit()
    {
        Application.Quit();
    }
    private void HowToPlay(bool active)
    {
        canvasAnimator.SetBool(hashHowToPlay, active);
    }
    private void Setting(bool active)
    {
        canvasAnimator.SetBool(hashSetting, active);
    }
    private void Credit(bool active)
    {
        canvasAnimator.SetBool(hashCredit, active);
    }
}
