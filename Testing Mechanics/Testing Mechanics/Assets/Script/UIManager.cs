using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    public static UIManager instance {get; private set;}

    public Image healthUI;
    public int count;
    public TMP_Text counterText;

    public GameObject killPrompt;
    public float promptTime;

    public Image gameOverPanel;
    public float fadeRate;
    public AudioSource gameOverAudio;

    public GameObject settingsWindow;

    [HeaderAttribute("Don't Simply Touch")]
    public int maxMobCount;

    private bool showDead;
    private float alphaLevel;
    private float maxAlpha = 1.0f;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        killPrompt.SetActive(false);
        showDead = false;
        alphaLevel = 0.0f;
        gameOverPanel.color = new Color(255, 255, 255, alphaLevel);
        settingsWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(showDead)
        {
            ShowDeadPanel();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && !settingsWindow.activeSelf)
        {
            Time.timeScale = 0;
            ActivateSettingWindow();
        }
    }

    public void UpdateHealthBar()
    {
        healthUI.fillAmount = PlayerManager.instance.playerHealth * 1.0f / PlayerManager.instance.playerMaxHealth;
        //Debug.Log(healthUI.fillAmount);
    }

    public void UpdateCounter()
    {
        count++;
        counterText.text = count.ToString();
    }

    public bool CompareCounters()
    {
        if(count == maxMobCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerator KillAllMobsPrompt()
    {
        killPrompt.SetActive(true);
        yield return new WaitForSeconds(promptTime);
        killPrompt.SetActive(false);
    }

    public void initiateDeadPanel()
    {
        showDead = true;
        gameOverAudio.Play();
        // play game over audio
    }

    private void ShowDeadPanel()
    {
        float speed = fadeRate * Time.deltaTime;

        if(alphaLevel < maxAlpha)
        {
            alphaLevel += fadeRate * Time.deltaTime;
            gameOverPanel.color = new Color(255, 255, 255, alphaLevel);
        }
    }

    public void ActivateSettingWindow()
    {
        settingsWindow.SetActive(true);
        LeanTween.cancel(settingsWindow);
        settingsWindow.transform.localScale = Vector3.zero;
        //LeanTween.scale(settingsWindow, new Vector3(2.0f, 0.05f, 1.0f), 0.1f)
        //    .setEaseInExpo();
        LeanTween.scale(settingsWindow, Vector3.one, 0.5f)
            .setEaseInOutBack()
            .setIgnoreTimeScale(true);
        //    .setDelay(0.1f);
    }

    public void CloseSettingWindow()
    {
        LeanTween.cancel(settingsWindow);
        LeanTween.scale(settingsWindow, Vector3.zero, 0.5f)
            .setEaseInOutBack()
            .setIgnoreTimeScale(true)
            .setOnComplete(offSettingWindow);
    }

    private void offSettingWindow()
    {
        settingsWindow.SetActive(false);
        Time.timeScale = 1.0f;
    }

}
