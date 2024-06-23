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

    private Vector2 startUnionPosition_L;
    private Vector2 startUnionPosition_R;

    [SerializeField] private Image image_L_Gauge;
    [SerializeField] private Image image_R_Gauge;
    private int L_Gauge;
    private int R_Gauge;
    private readonly float maxGauge = 100;

    [SerializeField] private GameObject[] unionPlayers;
    private GameObject unionpPlayer;

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
            StartUnion();
        }
    }


    public void SetUnionPlayer(GameObject player)
    {
        unionpPlayer = player;
    }
    private void InitUnionPlayer()
    {
        unionpPlayer = null;
    }

    public float GetLookNearPlayerAngle(Vector2 position)
    {
        Vector2 nearPosition = Vector2.zero;

        // 더 가까운 쪽의 플레이어를 가져옴
        nearPosition = GetNearPlayer(position);

        Vector2 direction = (position - nearPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        return angle;
    }

    private Vector2 GetNearPlayer(Vector2 offset)
    {
        if(unionpPlayer != null)
        {
            return unionpPlayer.transform.position;
        }

        float leftPlayerDistance;
        float rightPlayerDistance;

        // 플레이어와 받아온 포지션(적의 포지션)의 거리를 가져옴
        leftPlayerDistance = Vector2.Distance(offset, player_L_Transform.position);
        rightPlayerDistance = Vector2.Distance(offset, player_R_Transform.position);

        return leftPlayerDistance > rightPlayerDistance ? player_R_Transform.position : player_L_Transform.position;
    }

    public void StartUnion()
    {
        player_L.enabled = false;
        player_R.enabled = false;

        startUnionPosition_L = player_L_Transform.position;
        startUnionPosition_R = player_R_Transform.position;

        Vector2 centerVec = new Vector2((player_L_Transform.position.x + player_R_Transform.position.x) / 2,
            (player_L_Transform.position.y + player_R_Transform.position.y) / 2);

        StartCoroutine(MovePlayerCenter(centerVec));
    }

    private IEnumerator MovePlayerCenter(Vector2 center)
    {
        float duration = 0.5f;
        float time = 0;

        Vector2 initialPosition_L = player_L_Transform.position;
        Vector2 initialPosition_R = player_R_Transform.position;

        
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            float sineT = Mathf.Sin(t * Mathf.PI * 0.5f);

            player_L_Transform.position = Vector2.Lerp(initialPosition_L, center, sineT);
            player_R_Transform.position = Vector2.Lerp(initialPosition_R, center, sineT);

            yield return null;
        }

        player_L_Transform.position = center;
        player_R_Transform.position = center;

        Union();
    }

    public void ExitUnion()
    {
        SetActivePlayers(true);
        InitUnionPlayer();
        StartCoroutine(ReturnPlayer());
    }

    private IEnumerator ReturnPlayer()
    {
        player_L.enabled = true;
        player_R.enabled = true;

        float duration = 0.5f;
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            float sineT = Mathf.Sin(t * Mathf.PI * 0.5f);

            player_L_Transform.position = Vector2.Lerp(player_L_Transform.position, startUnionPosition_L, sineT);
            player_R_Transform.position = Vector2.Lerp(player_R_Transform.position, startUnionPosition_R, sineT);
            yield return null;
        }
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
        GameObject randomUnionPlayer = Instantiate(unionPlayers[randomIndex]);
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
