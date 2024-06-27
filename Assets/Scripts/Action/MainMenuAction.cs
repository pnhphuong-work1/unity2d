using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAction : MonoBehaviour
{
    public void PlayButton_OnClick()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void ExitButton_OnClick()
    {
        Application.Quit();
    }
    
}
