using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using YG;

public class MenuController : MonoBehaviour
{
    public string gameSceneName;

    public void PlayGame()
    {
        
        SceneManager.LoadScene(gameSceneName);
    }

    public void ExitGame()
    {
         #if UNITY_EDITOR
         EditorApplication.isPlaying = false;
         #else
         Application.Quit();
         #endif
    }
}
