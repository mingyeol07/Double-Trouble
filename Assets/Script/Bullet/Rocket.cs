// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

/// <summary>
/// ������ �����÷��̾��� �Ѿ� �����Ѿ�
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
