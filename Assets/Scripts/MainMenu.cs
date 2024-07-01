using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string playSceneName;
    public void Play() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(playSceneName);
    }

    public void Quit() {
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#else
                    Application.Quit();
#endif
    }
    // Start is called before the first frame update
    void Start() { Cursor.visible = true; }

    private void OnGUI() {
    }
}
