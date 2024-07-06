using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������ �÷��̾� ��Ʈ�ѷ�
/// </summary>
public class PlayerControlRight : Player
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rigid;
    [SerializeField] private Animator engineAnimator;
    private readonly int hashBoosting = Animator.StringToHash("Boosting");

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected override void MoveInput()
    {
        float h = Input.GetAxisRaw("HorizontalArrow");
        float v = Input.GetAxisRaw("VerticalArrow");

        rigid.velocity = new Vector2(h, v).normalized * moveSpeed;

        if (v > 0)
        {
            engineAnimator.SetBool(hashBoosting, true);
        }
        else
        {
            engineAnimator.SetBool(hashBoosting, false);
        }
    }
}
