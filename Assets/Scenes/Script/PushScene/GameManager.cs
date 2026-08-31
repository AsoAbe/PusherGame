using UnityEngine;

public class GameManager : MonoBehaviour
{
    // どこからでも使えるようにする
    public static GameManager Instance;

    // ゲーム開始時の借金額
    public const int INITIAL_DEBT = 1000;

    // ゲーム開始時の所持金
    public const int INITIAL_MONEY = 0;

    // ゲーム開始時のラウンド
    public const int INITIAL_ROUND = 1;

    // ゲーム開始時のターン
    public const int INITIAL_TURN = 1;

    // 1ゲームの最大ラウンド数
    public const int MAX_ROUND = 3;

    // 1ラウンドの最大ターン数
    public const int MAX_TURN = 3;

    // ゲームリセット時の借金額
    public const int RESET_DEBT = 30;
    
    // お金
    public int money;

    // 基本借金
    public int baseDebt = INITIAL_DEBT;

    // 借金
    public int debt = INITIAL_DEBT;

    // 総獲得数
    public int totalmoney = INITIAL_MONEY;

    // ラウンド
    public int round = INITIAL_ROUND;

    // 最大ラウンド
    public int maxRound = MAX_ROUND;

    // ターン
    public int turn = INITIAL_TURN;

    // 最大ターン
    public int maxTurn = MAX_TURN;


    //返済画面
    public GameObject DebtCanvas;
    private void Awake()
    {
        // すでに存在していたら消す
        if (Instance == null)
        {
            Instance = this;

            // シーンを切り替えても消えない
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ResetGame()
    {
        money = INITIAL_MONEY;

        baseDebt = RESET_DEBT;

        debt = RESET_DEBT;

        round = INITIAL_ROUND;

        turn = INITIAL_TURN;

        totalmoney = INITIAL_MONEY;
    }
}
