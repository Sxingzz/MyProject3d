using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponRecoil : MonoBehaviour
{
    [HideInInspector]
    public CinemachineFreeLook playerCamera;

    public float verticalRecoil;
    public float duration;

    private float time;


    public void GenerateRecoil()
    {
        time = duration;
    }

    private void Update()
    {
        if(time > 0)
        {
            playerCamera.m_YAxis.Value -= ((verticalRecoil/100) * Time.deltaTime) / duration;
            time -= Time.deltaTime;
        }
    }
}
