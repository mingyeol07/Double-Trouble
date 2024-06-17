using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [SerializeField] private Player player_L;
    [SerializeField] private Player player_R;
    private Transform player_L_Transform;
    private Transform player_R_Transform;

    [SerializeField] private Image image_L_Gauge;
    [SerializeField] private Image image_R_Gauge;
    private int L_Gauge;
    private int R_Gauge;
    private readonly float maxGauge = 100;

    [SerializeField] private GameObject[] unionPlayer; 

    private void Awake()
    {
        Instance = this;
        player_L_Transform = player_L.transform;
        player_R_Transform = player_R.transform;
    }

    private void Update()
    {
        if (L_Gauge >= maxGauge && R_Gauge >= maxGauge) 
        {
            L_Gauge = 0;
            R_Gauge = 0;
            image_L_Gauge.fillAmount = L_Gauge;
            image_R_Gauge.fillAmount = R_Gauge;
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
        SetActivePlayers(false);
        int randomIndex = Random.Range(0, 4);
        GameObject randomUnionPlayer = Instantiate(unionPlayer[randomIndex]);
        randomUnionPlayer.transform.position = player_L_Transform.position;
    }

    public void SetLeftPlayerGaugePlus()
    {
        L_Gauge++;
        image_L_Gauge.fillAmount = (float)L_Gauge / maxGauge;
    }

    public void SetRightPlayerGaugePlus()
    {
        R_Gauge++;
        image_R_Gauge.fillAmount = (float)R_Gauge / maxGauge;
    }
}
