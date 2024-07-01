using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Texture2D cursorTexture;
    public string playSceneName;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public void Play() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(playSceneName);
    }

    public void Quit() {
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#else
                    Application.Quit();
#endif
        Cursor.SetCursor(null, hotSpot, cursorMode);
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}
