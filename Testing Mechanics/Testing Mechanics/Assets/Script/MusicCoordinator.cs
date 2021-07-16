using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicCoordinator : MonoBehaviour
{
    public static MusicCoordinator instance;
    public AudioSource BGMplayer;
    public AudioClip baseBGM;
    public AudioClip bossBGM;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else if (instance != null || instance != this)
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        BasicMusicBGM();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BossMusicBGM()
    {
        BGMplayer.Stop();
        BGMplayer.clip = bossBGM;
        BGMplayer.Play();
    }

    public void BasicMusicBGM()
    {
        BGMplayer.Stop();
        BGMplayer.clip = baseBGM;
        BGMplayer.Play();
    }
}
