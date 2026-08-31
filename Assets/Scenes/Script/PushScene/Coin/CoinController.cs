using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CoinController : MonoBehaviour
{
    // コインPrefabを複数登録
    public GameObject[] Coin;
    public Transform CreatePoint;
    public float Power;

    public float Speed;

    public int CoinCount;
    public int BaseCoinCount;
    public Text CoinCountText;

    public Text MoneyText;

    public AudioSource audioSource;
    public AudioClip SE;

    // Input System
    private PlayerInputActions inputActions;
    private Vector2 moveInput;

    // プレイヤーの移動範囲
    private const float MIN_MOVE_X = -2.0f;
    private const float MAX_MOVE_X = 2.0f;

    // コイン生成時の回転角度
    private const float COIN_ROTATION_X = -90.0f;
    private const float COIN_ROTATION_Y = 0.0f;
    private const float COIN_ROTATION_Z = 0.0f;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        // 移動入力
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;

        // 発射入力
        inputActions.Player.Shoot.performed += Shoot;

        // タイトルに戻る
        inputActions.Player.Title.performed += ReturnToTitle;

        Debug.Log("Input Enabled");
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;

        inputActions.Player.Shoot.performed -= Shoot;

        inputActions.Player.Title.performed -= ReturnToTitle;

        inputActions.Disable();
    }

    private void Update()
    {
        Move();

        CoinCountText.text = CoinCount.ToString();
        MoneyText.text = GameManager.Instance.money.ToString();
    }

    //=========================
    // 移動入力
    //=========================
    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log(moveInput);
    }

    //=========================
    // 移動処理
    //=========================
    private void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0, 0);

        transform.position += move * Speed * Time.deltaTime;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, MIN_MOVE_X, MAX_MOVE_X);
        transform.position = pos;
    }

    //=========================
    // 発射処理
    //=========================
    private void Shoot(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (CoinCount == 0) return;

        if (GameManager.Instance.DebtCanvas.activeSelf)
            return;

        if (Coin.Length == 0)
            return;

        // ランダムにPrefabを選択
        int randomIndex = Random.Range(0, Coin.Length);

        // コイン生成
        GameObject createCoin = Instantiate(
            Coin[randomIndex],
            CreatePoint.position,
            Quaternion.Euler(
                COIN_ROTATION_X,
                COIN_ROTATION_Y,
                COIN_ROTATION_Z));

        // 発射
        Rigidbody rb = createCoin.GetComponent<Rigidbody>();

        rb.AddForce(
            CreatePoint.forward * Power,
            ForceMode.Impulse);

        // コイン消費
        CoinCount--;

        // 効果音
        audioSource.PlayOneShot(SE);
    }

    private void ReturnToTitle(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        SceneChanger.Instance.GoToReturnScen();
    }
}