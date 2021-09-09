using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreloadScript : MonoBehaviour
{
    public Image poster;
    public float fadeRate;
    public float preloadTime;

    private float alphaLevel;
    private float maxAlpha = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        alphaLevel = 0.0f;
        StartCoroutine("NextScene");
    }

    // Update is called once per frame
    void Update()
    {
        if(alphaLevel < maxAlpha)
        {
            alphaLevel += fadeRate * Time.deltaTime;
            poster.color = new Color(255, 255, 255, alphaLevel);
        }
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(preloadTime);
        SceneManager.LoadScene("Menu");
    }
}
