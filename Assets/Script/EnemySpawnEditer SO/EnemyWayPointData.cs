
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/EnemyWay Data")]
public class EnemyWayPointData : ScriptableObject
{
    [SerializeField] private Vector2 startPoint;
    public Vector2 StartPoint { get { return startPoint; } }

    [SerializeField] private Vector2 endPoint;
    public Vector2 EndPoint { get { return endPoint; } }
}
