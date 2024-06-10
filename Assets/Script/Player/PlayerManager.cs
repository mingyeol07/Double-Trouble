using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [SerializeField] private Transform player_L;
    [SerializeField] private Transform player_R;

    private void Awake()
    {
        Instance = this;
    }

    public Transform GetNearPlayerPosition(Transform transform)
    {
        Transform nearPosition = null;
        float leftPlayerDistance;
        float rightPlayerDistance;

        leftPlayerDistance = Vector2.Distance(transform.position, player_L.position);
        rightPlayerDistance = Vector2.Distance(transform.position, player_R.position);

        // 왼쪽 플레이어와의 거리가 더 멀다면 오른쪽 플레이어를 가져옴.
        nearPosition = leftPlayerDistance > rightPlayerDistance ? player_R : player_L;
        return nearPosition;
    }
}
