using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnionPlayer : Player
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float unionTime;
    [SerializeField] private Animator weaponAnim;
    private readonly int hashWeaponShot = Animator.StringToHash("Shot");
    private bool weaponShoting;
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

    protected override void ShotBullet()
    {
        base.ShotBullet();
        if(!weaponShoting)
        {
            weaponShoting = true;
            weaponAnim.SetTrigger(hashWeaponShot);
        }
    }

    protected abstract void UnionWeaponShot();

    private void Anim_ShotExit()
    {
        weaponShoting = false;
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
        Destroy(this.gameObject);
    }
}
