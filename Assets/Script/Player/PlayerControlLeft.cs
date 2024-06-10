using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlLeft : Player
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        MoveInput();
    }

    private void MoveInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigid.velocity = new Vector2(h, v).normalized * moveSpeed;
    }

    protected override void GameOver()
    {
        base.GameOver();
    }
}
