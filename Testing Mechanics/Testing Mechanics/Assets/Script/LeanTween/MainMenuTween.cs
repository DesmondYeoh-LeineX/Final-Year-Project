using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTween : MonoBehaviour
{
    public RectTransform mainMenu;
    public float animTime;

    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject exitButton;
    public GameObject title;

    private Vector3 breathe = new Vector3(1.05f, 1.05f, 1.0f);
    private float breatheTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        InTheBeginning();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InTheBeginning()
    {
        LeanTween.cancel(this.gameObject);
        transform.localScale = Vector3.zero;
        LeanTween.cancel(playButton);
        playButton.transform.localScale = Vector3.zero;
        LeanTween.cancel(settingsButton);
        settingsButton.transform.localScale = Vector3.zero;
        LeanTween.cancel(exitButton);
        exitButton.transform.localScale = Vector3.zero;
        LeanTween.cancel(title);
        title.transform.localScale = Vector3.zero;
        title.transform.position = new Vector3 (title.transform.position.x,
            title.transform.position.y + 50,
            title.transform.position.z);
        
        LeanTween.moveY(title, title.transform.position.y - 50, animTime);
        LeanTween.scale(title, new Vector3(2.0f, 0.05f, 1.0f), animTime)
            .setEaseInExpo();
        LeanTween.scale(title, Vector3.one, animTime)
            .setEaseInOutBack()
            .setDelay(animTime);
        
        LeanTween.scale(mainMenu, new Vector3(2.0f, 0.05f, 1.0f), animTime)
            .setEaseInExpo()
            .setDelay(animTime);
        LeanTween.scale(mainMenu, Vector3.one, animTime)
            .setEaseInOutBack()
            .setOnComplete(AnimTheButtons)
            .setDelay(animTime * 2);

        

    }

    void AnimTheButtons()
    {
        LeanTween.scale(title, breathe, breatheTime)
            .setLoopPingPong();
        LeanTween.scale(mainMenu, breathe, breatheTime)
            .setLoopPingPong();
        LeanTween.scale(exitButton, Vector3.one, animTime)
            .setEaseOutElastic();
        LeanTween.scale(settingsButton, Vector3.one, animTime)
            .setEaseOutElastic()
            .setDelay(animTime/2);
        LeanTween.scale(playButton, Vector3.one, animTime)
            .setEaseOutElastic()
            .setDelay(animTime);
    }

    
}
