using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
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
            onLeftPlayer = false;
        }
        else
        {
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
