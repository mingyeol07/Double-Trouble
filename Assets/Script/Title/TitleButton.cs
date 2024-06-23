using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public enum TitleButtonType
{
    Start, HowToPlay, Setting, Exit
}

public class TitleButton : MonoBehaviour
{
    [SerializeField] private TitleButtonType type;

    [SerializeField] private Image leftFill;
    [SerializeField] private Image rightFill;

    [SerializeField] private bool onLeftPlayer;
    [SerializeField] private bool onRightPlayer;

    private const int fillSpeed = 5;

    private void Update()
    {
        if(onLeftPlayer)
        {
            SetLeftFill(1);
        }
        else
        {
            SetLeftFill(0);
        }

        if(onRightPlayer)
        {
            SetRightFill(1);
        }
        else
        {
            SetRightFill(0);
        }

        if (leftFill.fillAmount >= 0.99f && rightFill.fillAmount >= 0.99f)
        {
            onLeftPlayer = false;
            onRightPlayer = false;
            TitleManager.Instance.ButtonEvent(type, true);
        }
        else
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<TitlePlayer>()?.GetLeftPosition() == true)
        {
            onLeftPlayer = true;
        }
        else
        {
            onRightPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<TitlePlayer>()?.GetLeftPosition() == true)
        {
            TitleManager.Instance.ButtonEvent(type, false);
            onLeftPlayer = false;
        }
        else
        {
            TitleManager.Instance.ButtonEvent(type, false);
            onRightPlayer = false;
        }
    }

    private void SetLeftFill(int targetInt)
    {
        leftFill.fillAmount = Mathf.Lerp(leftFill.fillAmount, targetInt, fillSpeed * Time.deltaTime);
    }

    private void SetRightFill(int targetInt)
    {
        rightFill.fillAmount = Mathf.Lerp(rightFill.fillAmount, targetInt, fillSpeed * Time.deltaTime);
    }
}
