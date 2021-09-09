using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider volume;
    //public AudioListener listener;

    private void Start() 
    {
        volume.value = AudioListener.volume;
    }

    public void SetVolume()
    {
        AudioListener.volume = volume.value;
    }
}
