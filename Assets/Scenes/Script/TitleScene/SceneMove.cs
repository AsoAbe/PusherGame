using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    void Update()
    {
        // SpaceƒL[‚ğ‰Ÿ‚µ‚½‚ç
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // GameScene‚ÖˆÚ“®
            SceneChanger.Instance.GoToPushScene();
        }
    }
}
