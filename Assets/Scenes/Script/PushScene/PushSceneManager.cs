using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PushSceneManager : MonoBehaviour
{
    public float timer = 30f;
    public Text TimerText;
    public float Bestimer = 30f;
    public GameObject DebtCanvas;

    // 通常のゲーム速度
    private const float NORMAL_TIME_SCALE = 1f;

    void Start()
    {
        // 毎回、新しいDebtCanvasを登録する
        GameManager.Instance.DebtCanvas = DebtCanvas;

        // 最初は非表示
        GameManager.Instance.DebtCanvas.SetActive(false);

        Time.timeScale = NORMAL_TIME_SCALE;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // 小数を消して表示
        TimerText.text =
           Mathf.Ceil(timer).ToString();

        // 時間切れ
        if (timer <= 0)
        {
            // タイマー停止
            timer = 0;

            // Debt画面表示
            GameManager.Instance.DebtCanvas.SetActive(true);

            // ゲーム停止
            Time.timeScale = 0;
        }
    }
}
