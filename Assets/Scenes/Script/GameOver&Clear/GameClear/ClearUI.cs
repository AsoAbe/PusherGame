using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ClearUI : MonoBehaviour
{
    [SerializeField] private GameObject titleButton;

    IEnumerator Start()
    {
        // UI‚Ì‰Šú‰»‚ªI‚í‚é‚Ü‚Å1ƒtƒŒ[ƒ€‘Ò‚Â
        yield return null;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(titleButton);
    }
}
