using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMini : MonoBehaviour
{
    private Transform target;

    public void SetTarget(Transform TargetTransform)
    {
        target = TargetTransform;
    }

    private void MoveToTarget()
    {

    }

    private void Update()
    {
        MoveToTarget();
    }


}
