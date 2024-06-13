// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;
using UnityEngine.UI;

public class PlayerTransformation : MonoBehaviour
{
    [SerializeField] private Image unionWaitTimeGauge;
    [SerializeField] private bool isLeft;
    private float unionTimer = 0;
    private const float unionDuration = 3.0f;
    private bool unionSet;
    private bool onStayPlayer;

    private void Update()
    {
        if (onStayPlayer)
        {
            if (unionTimer <= unionDuration && !unionSet)
            {
                unionTimer += Time.deltaTime;
                unionWaitTimeGauge.fillAmount = Mathf.Lerp(0, 1, unionTimer / unionDuration);

                if (unionTimer >= unionDuration)
                {
                    unionTimer = 0;
                    unionWaitTimeGauge.fillAmount = unionTimer;
                    SetAbleUnion();
                    unionSet = true;
                }
            }
        }
        else
        {
            unionSet = false;

            if (unionTimer > 0)
            {
                unionTimer -= Time.deltaTime * 5;
                unionWaitTimeGauge.fillAmount = Mathf.Lerp(0, 1, (unionTimer / unionDuration)); ;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onStayPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onStayPlayer = false;
        }
    }

    private void SetAbleUnion()
    {
        if(isLeft)
        {
            PlayerManager.Instance.SetLeftPlayerAbleUnion(true);
        }
        else
        {
            PlayerManager.Instance.SetRightPlayerAbleUnion(true);
        }
    }
}
