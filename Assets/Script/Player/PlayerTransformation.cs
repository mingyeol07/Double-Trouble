// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;
using UnityEngine.UI;

public class PlayerTransformation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
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
                playerSpriteRenderer.color = Color.Lerp(playerSpriteRenderer.color, new Color(0, 0, 0, 1), unionTimer / unionDuration);

                if (unionTimer >= unionDuration)
                {
                    unionTimer = 0;
                    playerSpriteRenderer.color = new Color(1, 1, 1, 1);
                    SetAbleUnion();
                    unionSet = true;
                }
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
            unionTimer = 0;
            playerSpriteRenderer.color = new Color(1, 1, 1, 1);
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
