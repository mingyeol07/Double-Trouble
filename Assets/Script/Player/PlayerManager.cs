using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [SerializeField] private Player player_L;
    [SerializeField] private Player player_R;
    private Transform player_L_Transform;
    private Transform player_R_Transform;

    [SerializeField] private UnionPlayer player_union; 

    private void Awake()
    {
        Instance = this;
        player_L_Transform = player_L.transform;
        player_R_Transform = player_R.transform;
    }

    public float GetLookNearPlayerAngle(Vector2 position)
    {
        Vector2 nearPosition = Vector2.zero;
        float leftPlayerDistance;
        float rightPlayerDistance;

        leftPlayerDistance = Vector2.Distance(position, player_L_Transform.position);
        rightPlayerDistance = Vector2.Distance(position, player_R_Transform.position);

        // 왼쪽 플레이어와의 거리가 더 멀다면 오른쪽 플레이어를 가져옴.
        nearPosition = leftPlayerDistance > rightPlayerDistance ? player_R_Transform.position : player_L_Transform.position;

        Vector2 direction = (position - nearPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        return angle;
    }
    public void SetActivePlayers(bool isActive)
    {
        player_L.gameObject.SetActive(isActive);
        player_R.gameObject.SetActive(isActive);
    }

    public void Union()
    {
        player_union.gameObject.SetActive(true);
    }
}
