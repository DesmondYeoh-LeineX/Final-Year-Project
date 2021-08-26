using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSoundEffect : MonoBehaviour
{
    public AudioSource sfxPlayer;
    public AudioClip runSFX;
    public AudioClip jumpSFX;
    public AudioClip wallSlideSFX;
    public AudioClip healSFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RunningSFX()
    {
        sfxPlayer.PlayOneShot(runSFX);
    }

    public void JumpSFX()
    {
        sfxPlayer.PlayOneShot(jumpSFX);
    }

    public void WallSlideSFX()
    {
        sfxPlayer.PlayOneShot(wallSlideSFX);
    }

    public void HealSFX()
    {
        sfxPlayer.PlayOneShot(healSFX);
    }
}
