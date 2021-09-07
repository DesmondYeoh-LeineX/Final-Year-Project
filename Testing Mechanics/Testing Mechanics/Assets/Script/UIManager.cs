using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance {get; private set;}

    public Image healthUI;
    public int count;
    public TMP_Text counterText;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
