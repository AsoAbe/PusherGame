using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameExit : MonoBehaviour
{
    public void ExitGame()
    {
        //Debug.Log("èIóπèàóù");
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
