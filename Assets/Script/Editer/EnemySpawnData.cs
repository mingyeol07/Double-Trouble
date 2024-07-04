using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// JsonData로 저장할 클래스
/// </summary>
[Serializable]
public class EnemySpawnData
{
    public int spawnTime;
    public EnemyType enemyType;
    public IntVector2 startPos;
    public IntVector2 endPos;

    [Serializable]
    public class IntVector2
    {
        public int x;
        public int y;

        public IntVector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
