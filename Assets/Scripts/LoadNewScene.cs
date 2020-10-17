using UnityEngine;
using UnityEngine.SceneManagement;

//For transitioning to a new scene
public class LoadNewScene : MonoBehaviour
{
    [Tooltip("What is the name of the scene we want to load when clicking the button?")]
    public string SceneName;

    public void LoadTargetScene()
    {
        SceneManager.LoadSceneAsync(SceneName);
    }
}
