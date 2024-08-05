using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// 플레이어에 관련된 모든 것을 관리하는 매니저 (합체, 스타트, 게임오버 등)
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public Player player_L;
    public Player player_R;
    private Transform player_L_Transform;
    private Transform player_R_Transform;

    private Vector2 startUnionPosition_L;
    private Vector2 startUnionPosition_R;

    [SerializeField] private Image image_L_Gauge;
    [SerializeField] private Image image_R_Gauge;
    private int L_Gauge;
    private int R_Gauge;

    public bool isPlay {  get; private set; }
    [SerializeField] private GameObject txt_Start;
    [SerializeField] private GameObject txt_Clear;
    [SerializeField] private GameObject pnl_GameOver;
    [SerializeField] private GameObject pnl_GameClear;
    [SerializeField] private int maxGauge = 100;

    [SerializeField] private GameObject[] playerHpImage_L;
    [SerializeField] private GameObject[] playerHpImage_R;
    [SerializeField] private GameObject[] unionPlayers;
    private GameObject unionpPlayer;
    private bool IsCheat;

    [SerializeField] private Image pnl_fade;

    [SerializeField] private TMP_Text txt_score;
    [SerializeField] private TMP_Text txt_score2;
    [SerializeField] private TMP_Text txt_score3;

    private int score;

    private int randomIndex = 4;

    private void Awake()
    {
        Instance = this;
        isPlay = false;
        player_L_Transform = player_L.transform;
        player_R_Transform = player_R.transform;
    }

    private void Start()
    {
        StartCoroutine(fadeIn());
        player_L.GetComponent<Animator>().SetTrigger("Start");
        player_R.GetComponent<Animator>().SetTrigger("Start");
        StartCoroutine(LeadyStart());
    }

    private IEnumerator LeadyStart()
    {
        yield return new WaitForSeconds(1);
        player_L.StartSetup();
        player_R.StartSetup();
        isPlay = true;
        txt_Start.SetActive(true);
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

        if(Input.GetKeyDown(KeyCode.F1))
        {
            IsCheat = !IsCheat;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            randomIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            randomIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            randomIndex = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            randomIndex = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            randomIndex = 4;
        }

        txt_score.text = score.ToString();
        txt_score3.text = "SCORE: " + score.ToString();
        txt_score2.text = "SCORE: " + score.ToString();
    }

    public void ScoreUp(int upScale)
    {
        score += upScale;
    }

    public void Stage1Clear()
    {
        StartCoroutine(NextStage());
    }

    public void Stage2Clear()
    {
        StartCoroutine(GameClear());
    }

    private IEnumerator fadeIn()
    {
        float time = 0;
        float fadeDuration = 2;

        // 페이드 인 (알파 값 감소)
        time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            pnl_fade.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    private IEnumerator NextStage()
    {
        isPlay = false;
        txt_Clear.SetActive(true);
        yield return new WaitForSeconds(1f);

        // 페이드 아웃 (알파 값 증가)
        float fadeDuration = 2f;
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, time / fadeDuration);
            pnl_fade.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        

        SceneManager.LoadScene("Stage1");
    }

    private IEnumerator GameClear()
    {
        isPlay = false;
        txt_Clear.SetActive(true);
        yield return new WaitForSeconds(3f);
        pnl_GameClear.SetActive(true);
    }

    public void PlayerDestroy(int hp, bool isLeft)
    {
        if (IsCheat) return;

        GameObject player;

        if (isLeft)
        {
            player = player_L.gameObject;
            playerHpImage_L[hp].SetActive(false);
            player.transform.position = new Vector2(-4, -6);
        }
        else
        {
            player = player_R.gameObject;
            playerHpImage_R[hp].SetActive(false);
            player.transform.position = new Vector2(4, -6);
        }

        if (hp <= 0)
        {
            SetActivePlayers(false);
            GameOver();
            return;
        }

        player.SetActive(false);
        StartCoroutine(Respawn(player));
    }

    private IEnumerator Respawn(GameObject player)
    {
        yield return new WaitForSeconds(1f);

        player.SetActive(true);
        player.GetComponent<Animator>().SetTrigger("Start");
    }

    private void GameOver()
    {
        isPlay = false;
        pnl_GameOver.SetActive(true);
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
        player_L.isShield = true;
        player_R.isShield = true;

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
        player_L.isShield = true;
        player_R.isShield = true;

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
        if(player_L.gameObject.activeSelf && player_R.gameObject.activeSelf)
        {
            int ran;
            SetActivePlayers(false);

            if (randomIndex == 4) ran = Random.Range(0, 4);
            else ran = randomIndex;
            
            GameObject randomUnionPlayer = Instantiate(unionPlayers[ran]);
            randomUnionPlayer.transform.position = player_L_Transform.position;
        }
    }

    public void SetLeftPlayerGaugePlus()
    {
        if(player_L.gameObject.activeSelf)
        {
            L_Gauge++;
            image_L_Gauge.fillAmount = (float)L_Gauge / maxGauge;
        }
    }

    public void SetRightPlayerGaugePlus()
    {
        if (player_R.gameObject.activeSelf)
        {
            R_Gauge++;
            image_R_Gauge.fillAmount = (float)R_Gauge / maxGauge;
        }
    }
}
