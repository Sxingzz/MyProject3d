using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent: UnityEvent <string>
{

}

public class WeaponAnimationEvent : MonoBehaviour
{
    public AnimationEvent WeaponAnimEvent = new AnimationEvent();
    public void OnAniamtionEvent(string eventName)
    {
        WeaponAnimEvent.Invoke(eventName);
    }

    public void AudioOnShootLaser()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_LASER2);
        }
    }

    public void AudioOnshootPistol()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_LASER1);
        }
    }

}
