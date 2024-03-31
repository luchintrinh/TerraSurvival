using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    public float shakeIntensity = 1f;
    public float shakeDuration = 1f;

    private float timer;

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Call this method to initiate screen shake
    public void ShakeScreen()
    {
        CinemachineBasicMultiChannelPerlin _cbmp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmp.m_AmplitudeGain = shakeIntensity;
        timer = shakeDuration;
    }
    public void StopShake()
    {
        CinemachineBasicMultiChannelPerlin _cbmp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmp.m_AmplitudeGain = 0;
        timer = 0;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StopShake();
            }
        }
    }
}
