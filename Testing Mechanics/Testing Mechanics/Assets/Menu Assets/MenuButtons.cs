using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public MainMenuTween tweenScript;

    private void Start() 
    {
        MusicCoordinator.instance.BasicMusicBGM();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void SettingButton()
    {
        tweenScript.ActivateSettingWindow();
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SettingBackButton()
    {
        tweenScript.CloseSettingWindow();
    }
}
