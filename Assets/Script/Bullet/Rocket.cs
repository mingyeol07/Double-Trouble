// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

/// <summary>
/// 로켓형 변신플레이어의 총알 로켓총알
/// </summary>
public class Rocket : Bullet
{
    [SerializeField] private GameObject beam;

    protected override void Destroy()
    {
        base.Destroy();
        GameObject go = Instantiate(beam);
        go.transform.position = transform.position;
    }
}
