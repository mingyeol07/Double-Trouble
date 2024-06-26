using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditerSetEnemyDropDown : MonoBehaviour
{
    public EnemyType enemyType;

    public void OnDropDownEvent(int index)
    {
        enemyType = (EnemyType)index;
        Debug.Log(enemyType);
    }
}
