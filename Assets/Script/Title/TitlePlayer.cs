using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitlePlayer : MonoBehaviour
{
    [SerializeField] private bool isLeft;
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveInput();
    }

    private void MoveInput()
    {
        if (transform.position.x >= 9)
        {
            transform.position = new Vector2(9, transform.position.y);
        }
        else if(transform.position.x <= -9)
        {
            transform.position = new Vector2(-9, transform.position.y);
        }

        if (transform.position.y >= 5)
        {
            transform.position = new Vector2(transform.position.x, 5);
        }
        else if (transform.position.y<= -5)
        {
            transform.position = new Vector2(transform.position.x, -5);
        }

        float h = isLeft ? Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("HorizontalArrow");
        float v = isLeft ? Input.GetAxisRaw("Vertical") : Input.GetAxisRaw("VerticalArrow");

        rigid.velocity = new Vector2(h, v).normalized * moveSpeed;
    }

    public bool GetLeftPosition()
    {
        return isLeft;
    }
}
