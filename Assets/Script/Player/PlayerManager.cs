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

        // ���� �÷��̾���� �Ÿ��� �� �ִٸ� ������ �÷��̾ ������.
        nearPosition = leftPlayerDistance > rightPlayerDistance ? player_R : player_L;
        return nearPosition;
    }
}
