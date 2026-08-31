using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebtUI : MonoBehaviour
{
    public Text MoneyCount;
    public Text DebtCount;
    public Text TurnCount;
    public Text RoundCount;

    public CoinController coinController;
    // 借金の加算
    public int debtAdd;
    public InputField PayInputField;

    public GameObject DebtCanvas;
    public PushSceneManager pushSceneManager;

    private PlayerInputActions inputActions;

    // 返済額の増減値
    public int addMoney = 1;

    // 残りターン数がこの値以下になった場合に警告表示する
    private const int REMAINING_TURN_WARNING = 1;

    // ラウンド開始時のターン番号
    private const int ROUND_START_TURN = 1;

    // ターン数の警告表示に使用する点滅速度
    private const float WARNING_BLINK_SPEED = 5f;

    // 点滅の切り替え基準値
    private const float BLINK_THRESHOLD = 0.5f;

    // 通常時のゲーム速度
    private const float NORMAL_TIME_SCALE = 1f;

    // 返済額の最小値
    private const int MIN_PAY_AMOUNT = 0;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Repayment.Enable();

        inputActions.Repayment.Increase.performed += IncreasePay;
        inputActions.Repayment.Decrease.performed += DecreasePay;
        inputActions.Repayment.Confirm.performed += ConfirmPay;
    }

    private void OnDisable()
    {
        inputActions.Repayment.Increase.performed -= IncreasePay;
        inputActions.Repayment.Decrease.performed -= DecreasePay;
        inputActions.Repayment.Confirm.performed -= ConfirmPay;

        inputActions.Repayment.Disable();
    }

    void Update()
    {
        MoneyCount.text =
            GameManager.Instance.money.ToString();

        DebtCount.text =
            GameManager.Instance.debt.ToString();

        int remainTurn =
            GameManager.Instance.maxTurn - 
            GameManager.Instance.turn + 1;

        TurnCount.text =
            remainTurn.ToString();

        if (remainTurn <= REMAINING_TURN_WARNING) // 残り1ターン
        {
            float blink = Mathf.Abs(
            Mathf.Sin(Time.unscaledTime * WARNING_BLINK_SPEED)
        );

            if (blink > BLINK_THRESHOLD)
            {
                TurnCount.color = Color.red;
            }
            else
            {
                TurnCount.color = Color.white;
            }
        }
        else
        {
            TurnCount.color = Color.white;
        }

        RoundCount.text =
            GameManager.Instance.round.ToString();

    }

    // ⑤ 入力処理
    private void IncreasePay(InputAction.CallbackContext ctx)
    {
        int pay = 0;

        int.TryParse(PayInputField.text, out pay);

        pay += addMoney;

        if (pay > GameManager.Instance.money)
            pay = GameManager.Instance.money;

        if (pay > GameManager.Instance.debt)
            pay = GameManager.Instance.debt;

        PayInputField.text = pay.ToString();
    }

    private void DecreasePay(InputAction.CallbackContext ctx)
    {
        int pay = 0;

        int.TryParse(PayInputField.text, out pay);

        pay -= addMoney;

        if (pay < MIN_PAY_AMOUNT)
            pay = MIN_PAY_AMOUNT;

        PayInputField.text = pay.ToString();
    }

    private void ConfirmPay(InputAction.CallbackContext ctx)
    {
        PayDebt();
    }

    public void PayDebt()
    {
        // 入力が数字かどうかチェック outからint payの宣言
        if (!int.TryParse(PayInputField.text, out int pay))
        {
            Debug.Log("数字を入力してください");
            PayInputField.text = "";
            return;
        }
        

        int originalPay = pay;

        // 所持金を超えたら所持金と同数にする
        if (pay > GameManager.Instance.money)
        {
            pay = GameManager.Instance.money;
        }

        // 借金を超えたら借金額にする
        if (pay > GameManager.Instance.debt)
        {
            pay = GameManager.Instance.debt;
        }

        // 補正が発生したら表示だけ更新して終了
        if (pay != originalPay)
        {
            PayInputField.text = pay.ToString();
            return;
        }


        // ここから実際の返済
        // お金減少
        GameManager.Instance.money -= pay;

        // 借金減少
        GameManager.Instance.debt -= pay;

        // 完済チェック
        if (GameManager.Instance.debt <= 0)
        {
            NextRound();
        }
        else
        {
            NextTurn();
        }
    }

    void NextTurn()
    {
        GameManager.Instance.turn++;

        // 制限ターン超え
        if (GameManager.Instance.turn >
           GameManager.Instance.maxTurn)
        {
            GameOver();
        }
        else
        {
            //コインの足りない分だけ補充,追加
            GameCoinAdd();
            pushSceneManager.timer = pushSceneManager.Bestimer;

            Time.timeScale = NORMAL_TIME_SCALE;

            DebtCanvas.SetActive(false);
        }
    }

    void NextRound()
    {
        GameManager.Instance.round++;
        // 制限ラウンド超え
        if (GameManager.Instance.round >
           GameManager.Instance.maxRound)
        {
            // 完済チェック
            if (GameManager.Instance.debt <= 0)
            {
                GameClear();
            }
            else
            {
                GameOver();
            }
        }
        else
        {
            GameManager.Instance.turn = ROUND_START_TURN;

            // 次Roundの借金増加
            GameManager.Instance.baseDebt += debtAdd;

            // 現在借金に追加
            GameManager.Instance.debt +=
                GameManager.Instance.baseDebt;

            //コインの足りない分だけ補充,追加
            GameCoinAdd();

            pushSceneManager.timer = pushSceneManager.Bestimer;

            Time.timeScale = NORMAL_TIME_SCALE;

            GameManager.Instance.DebtCanvas.SetActive(false);
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    void GameClear()
    {
        SceneManager.LoadScene("ClearScene");
    }

    void GameCoinAdd()
    {
        if (coinController.CoinCount < coinController.BaseCoinCount)
        {
            coinController.CoinCount = coinController.BaseCoinCount;
        }
    }
}