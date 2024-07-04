using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Editer에서 EnemyType을 정하는 드롭다운
/// </summary>
public class EditorSetEnemyDropDown : MonoBehaviour
{
    public EnemyType enemyType {  get; private set; }

    public void OnDropDownEvent(int index)
    {
        enemyType = (EnemyType)index;
        Debug.Log(enemyType);
    }
}
