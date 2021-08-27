using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSoundEffect : MonoBehaviour
{
    [HeaderAttribute("Basic")]
    public AudioSource sfxPlayer;
    public AudioClip runSFX;
    public AudioClip jumpSFX;
    public AudioClip wallSlideSFX;
    public AudioClip healSFX;
    public AudioClip dashSFX;

    [HeaderAttribute("Fighting SFX")]
    public AudioClip punch1SFX;
    public AudioClip punch2SFX;
    public AudioClip punch3SFX;
    public AudioClip damagedSFX;


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

    public void DashSFX()
    {
        sfxPlayer.PlayOneShot(dashSFX);
    }

    public void Punch1SFX()
    {
        sfxPlayer.PlayOneShot(punch1SFX);
    }

    public void Punch2SFX()
    {
        sfxPlayer.PlayOneShot(punch2SFX);
    }

    public void Punch3SFX()
    {
        sfxPlayer.PlayOneShot(punch3SFX);
    }

    public void DamagedSFX()
    {
        sfxPlayer.PlayOneShot(damagedSFX);
    }

}
