using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorSetEnemyDropDown : MonoBehaviour
{
    public EnemyType enemyType {  get; private set; }

    public void OnDropDownEvent(int index)
    {
        enemyType = (EnemyType)index;
        Debug.Log(enemyType);
    }
}
