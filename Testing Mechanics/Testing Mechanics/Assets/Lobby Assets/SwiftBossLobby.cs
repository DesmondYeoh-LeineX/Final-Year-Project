using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SwiftBossLobby : MonoBehaviour
{
    public void GoToSwiftLevel()
    {
        SceneManager.LoadScene("Loading");
    }
}
