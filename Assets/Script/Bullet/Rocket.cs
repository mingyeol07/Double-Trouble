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
    [SerializeField] private Transform beamPos;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            GameObject go = Instantiate(beam);
            go.transform.position = beamPos.position;
            Destroy(go, 0.9f);
            
            Destroy();
        }
    }
}
