using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // どこからでも使えるようにする
    public static SceneChanger Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void GoToTitleScene()
    {
        // ゲームの状態を初期化
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("TitleScene");
    }

    public void GoToDebtScene()
    {
        SceneManager.LoadScene("DebtScene");
    }

    public void GoToPushScene()
    {
        SceneManager.LoadScene("PushScene");
    }

    public void GoToClearScene()
    {
        SceneManager.LoadScene("ClearScene");
    }
    public void GoToGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void GoToReturnScen()
    {
        //PushSceneからタイトルに戻るボタンを押した時の処理リザルトでは使わない

        //オブジェクトを破棄してゲームをリセットする
        GameManager.Instance.DebtCanvas.SetActive(false);

        //DontDestroyOnLoad(gameObject);
        // ゲームの状態を初期化
        GameManager.Instance.ResetGame();

        // タイトルへ戻る
        SceneManager.LoadScene("TitleScene");
    }
}
