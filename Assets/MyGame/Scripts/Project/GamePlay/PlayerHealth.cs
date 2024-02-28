﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class PlayerHealth : Health
{
    private Ragdoll ragdoll;
    private ActiveWeapon activeWeapon;
    private CharacterAiming characterAiming;
    private VolumeProfile postProcessing;
    private Vignette vignette;


    protected override void OnStart()
    {
        ragdoll = GetComponent<Ragdoll>();
        activeWeapon = GetComponent<ActiveWeapon>();
        characterAiming = GetComponent<CharacterAiming>();
        postProcessing = FindObjectOfType<Volume>().profile;

        if (DataManager.HasInstance)
        {
            maxHealth = DataManager.Instance.DataConfig.PlayermaxHealth;
            currentHealth = maxHealth;
        }
    }
    protected override void OnDamaged(Vector3 direction)
    {
        if (postProcessing.TryGet(out vignette))
        {
            float percent = 1.0f - (currentHealth / maxHealth);
            vignette.intensity.value = percent * 0.4f;
        }
    }
    protected override void OnDeath(Vector3 direction)
    {
        ragdoll.ActiveRagdoll();
        direction.y = 1f;
        ragdoll.ApplyFore(direction); // áp lực khi chết để văng lên
        activeWeapon.DropWeapon();
        characterAiming.enabled = false; // để khi chết khỏi xoay
        if (CameraManager.HasInstance)
        {
            CameraManager.Instance.EnableKillCam();
        }

        DOVirtual.DelayedCall(2f, () =>
        {
            if (UIManager.HasInstance)
            {
                string message = "Lose";
                UIManager.Instance.ShowPopup<PopupMessage>(data: message);
            }
        });
    }

    // overrider hàm onheath
}