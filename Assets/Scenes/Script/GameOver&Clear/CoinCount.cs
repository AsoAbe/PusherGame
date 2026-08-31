using System.Collections;
using UnityEngine;
using TMPro;

public class CoinCount : MonoBehaviour
{
    public TextMeshProUGUI resultText;

    // メダルPrefab複数
    public GameObject[] medalPrefabs;

    // メダル生成位置
    public Transform spawnPoint;

    // 総獲得メダル数
    public int result;

    // 落下範囲
    public float rangeX = 5f;

    // GameManagerが存在しない場合に使用するリザルトの仮の値
    public const int DEFAULT_RESULT = 500;

    // リザルトのカウント開始値
    public const int INITIAL_COUNT = 0;

    // リザルトの数字を更新する間隔（秒）
    public const float COUNT_INTERVAL = 0.01f;

    IEnumerator Start()
    {

        result = DEFAULT_RESULT;

        // GameManagerがある時
        if (GameManager.Instance != null)
        {
            result = GameManager.Instance.totalmoney;
        }

        int count = INITIAL_COUNT;

        while (count <= result)
        {
            // 数字更新
            resultText.text =
                "総獲得メダル : " + count;

            Debug.Log("count = " + count);
            count++;

            yield return new WaitForSeconds(COUNT_INTERVAL);
            Debug.Log("CreateMedal");
        }
    }
    void Update()
    {
        
    }
}
