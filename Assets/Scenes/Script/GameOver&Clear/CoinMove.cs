using UnityEngine;

public class MedalSpin : MonoBehaviour
{
    // メダルの回転速度
    public const float SPIN_SPEED = 10f;

    // メダルを削除するまでの時間（秒）
    public const float DESTROY_TIME = 5f;

    void Start()
    {
        Rigidbody rb =
            GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.angularVelocity =
                Random.insideUnitSphere * SPIN_SPEED;
        }

        Destroy(gameObject, DESTROY_TIME);
    }
}