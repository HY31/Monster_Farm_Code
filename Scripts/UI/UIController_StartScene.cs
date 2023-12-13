using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController_StartScene : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;

    SoundManager soundManager;

    private void Awake()
    {
        soundManager = SoundManager.instance;
    }

    public void OpenSettingsMenu()
    {
        soundManager.PlayClickEffect();
        settingsMenu.SetActive(true);
    }

    public void CloseSettingsMenu() 
    {
        soundManager.PlayClickEffect();
        settingsMenu.SetActive(false);
    }

    public void EnterGameLobby()
    {
        soundManager.PlayClickEffect();
        soundManager.StopBGM();
        SceneLoad.LoadScene("GameLobby");
    }

   public void ExitGame()
    {
        soundManager.PlayClickEffect();
        Application.Quit();
    }

}
