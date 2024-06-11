// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;
using UnityEngine.UI;

public class TitleHowToPlayPanel : MonoBehaviour
{
    [SerializeField] private Image howToPlayImage = null;
    [SerializeField] private Sprite[] howToPlaySprites = null;
    [SerializeField] private Button btn_left;
    [SerializeField] private Button btn_right;
    private int imageIndex = 0;

    private void Awake()
    {
        btn_left.onClick.AddListener(OnClickMoveLeftImage);
        btn_right.onClick.AddListener(OnClickMoveRightImage);
    }

    private void OnClickMoveLeftImage()
    {
        if (imageIndex == 0)
        {
            imageIndex = howToPlaySprites.Length - 1;
        }
        else imageIndex--;

        howToPlayImage.sprite = howToPlaySprites[imageIndex];
    }

    private void OnClickMoveRightImage()
    {
        if (imageIndex == howToPlaySprites.Length - 1)
        {
            imageIndex = 0;
        }
        else imageIndex++;

        howToPlayImage.sprite = howToPlaySprites[imageIndex];
    }
}
