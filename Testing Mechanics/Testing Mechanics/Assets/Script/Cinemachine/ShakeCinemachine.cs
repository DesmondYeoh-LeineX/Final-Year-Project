using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeCinemachine : MonoBehaviour
{
    [HeaderAttribute("Shake Values")]
    public float intensity;    
    public float shaketime;

    private CinemachineVirtualCamera vCam;
    private CinemachineBasicMultiChannelPerlin perlin;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        perlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        timer = 0.0f;
        OffShake();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                OffShake();
            }
        }   
    }

    public void ShakeCamera()
    {
        perlin.m_AmplitudeGain = intensity;
        ResetShakeTime();
    }

    public void OffShake()
    {
        perlin.m_AmplitudeGain = 0.0f;
    }

    public void ResetShakeTime()
    {
        timer = shaketime;
    }
}
