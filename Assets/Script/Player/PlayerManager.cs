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
    private bool player_left_AbleUnion;
    private bool player_right_AbleUnion;

    [SerializeField] private GameObject[] player_union; 

    private void Awake()
    {
        Instance = this;
        player_L_Transform = player_L.transform;
        player_R_Transform = player_R.transform;
    }

    private void Update()
    {
        if (player_left_AbleUnion && player_right_AbleUnion) {
            Union();
        }
    }

    public float GetLookNearPlayerAngle(Vector2 position)
    {
        Vector2 nearPosition = Vector2.zero;
        float leftPlayerDistance;
        float rightPlayerDistance;

        // 플레이어와 받아온 포지션(적의 포지션)의 거리를 가져옴
        leftPlayerDistance = Vector2.Distance(position, player_L_Transform.position);
        rightPlayerDistance = Vector2.Distance(position, player_R_Transform.position);

        // 더 가까운 쪽의 플레이어를 가져옴
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
        player_left_AbleUnion = false;
        player_right_AbleUnion = false;

        SetActivePlayers(false);
        int randomIndex = Random.Range(0, 4);
        GameObject unionPlayer = Instantiate(player_union[randomIndex]);
        unionPlayer.transform.position = player_L_Transform.position;
    }

    public void SetLeftPlayerAbleUnion(bool isAble)
    {
        player_left_AbleUnion = isAble;
    }

    public void SetRightPlayerAbleUnion(bool isAble)
    {
        player_right_AbleUnion = isAble;
    }
}
