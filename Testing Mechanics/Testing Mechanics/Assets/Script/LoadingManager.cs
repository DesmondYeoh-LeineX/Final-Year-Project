using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public Animator anim;
    public float waitTime = 10.0f;
    public bool readyToReceiveInput;

    // Start is called before the first frame update
    void Start()
    {
        readyToReceiveInput = false;
        StartCoroutine("LoadTime");
    }

    // Update is called once per frame
    void Update()
    {
        if(readyToReceiveInput && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("BetaLevel");
        }
    }

    public void ReadyToGoNext()
    {
        readyToReceiveInput = true;
    }

    private IEnumerator LoadTime()
    {
        yield return new WaitForSeconds(waitTime);
        anim.SetTrigger("TransitionToReady");
    }
}
