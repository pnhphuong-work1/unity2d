using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAction : MonoBehaviour
{
    public void PlayButton_OnClick()
    {
        DataPersistenceManager.Instance.NewGame();
    }
    public void LoadButton_OnClick()
    {
        DataPersistenceManager.Instance.LoadGame();
    }
    public void SettingButton_OnClick()
    {
        
    }
    public void ExitButton_OnClick()
    {
        
    }
    
}
