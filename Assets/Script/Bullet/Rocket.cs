// # Systems
using System.Collections;
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class Rocket : Bullet
{
    [SerializeField] private GameObject beam;

    protected override void Destroy()
    {
        base.Destroy();
        Instantiate(beam, transform.position, Quaternion.identity);
    }
}
