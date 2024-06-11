using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnionPlayer : Player
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float unionTime;
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Co_StartUnionTime());
    }

    protected override void Update()
    {
        base.Update();
        MoveInput();
    }

    private void MoveInput()
    {
        float h = Input.GetAxisRaw("HorizontalMultiple");
        float v = Input.GetAxisRaw("VerticalMultiple");

        rigid.velocity = new Vector2(h, v).normalized * moveSpeed;
    }

    protected override void GameOver()
    {
        
    }

    private IEnumerator Co_StartUnionTime()
    {
        PlayerManager.Instance.SetActivePlayers(false);
        yield return new WaitForSeconds(unionTime);
        PlayerManager.Instance.SetActivePlayers(true);
        gameObject.SetActive(false);
    }
}
