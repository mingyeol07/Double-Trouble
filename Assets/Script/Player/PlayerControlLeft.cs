using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlLeft : Player
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rigid;
    [SerializeField] private Animator engineAnimator;
    private readonly int hashBoosting = Animator.StringToHash("Boosting");

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

        if(v > 0)
        {
            engineAnimator.SetBool(hashBoosting, true);
        }
        else
        {
            engineAnimator.SetBool(hashBoosting, false);
        }
    }
}
