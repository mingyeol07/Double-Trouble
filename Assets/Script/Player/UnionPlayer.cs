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
        yield return new WaitForSeconds(unionTime);
        PlayerManager.Instance.SetActivePlayers(true);
        gameObject.SetActive(false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Co_StartUnionTime());
    }
}
