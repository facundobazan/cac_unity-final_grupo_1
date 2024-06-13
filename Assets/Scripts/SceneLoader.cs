using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadScene(SceneNames scene)
    {
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
    }

    public static void LoadSceneAsync(SceneNames scene)
    {
        SceneManager.LoadSceneAsync(scene.ToString());
    }

    public static void LoadSceneAdditive(SceneNames scene)
    {
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);
    }

    public static void UnloadScene(SceneNames scene)
    {
        SceneManager.UnloadSceneAsync(scene.ToString());
    }
}
