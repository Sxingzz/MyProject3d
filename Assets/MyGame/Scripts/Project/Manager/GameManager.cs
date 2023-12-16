using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    private float notifyLoadingTime = 5f;

    void Start()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowNotify<NotifyLoading>();
            NotifyLoading scr = UIManager.Instance.GetExistNotify<NotifyLoading>();
            if (scr != null)
            {
                scr.AnimationLoaddingText(notifyLoadingTime);
                scr.DoAnimationLoadingProgress(notifyLoadingTime,
                    OnComplete: () =>
                    {
                        Debug.Log("NotifyLoading Complete");
                        scr.Hide();
                        UIManager.Instance.ShowScreen<ScreenHome>();
                    });
            }
        }

        // ví dụ về cách sử dụng action và các hàm liên quan
        //StartCoroutine(TestCoroutine(OnTestStart, OnTestComplete));
    }

    //Void OnNotifyLoadingComplete()
    //{
    //    Debug.log("NotifyLoading Complete ");
    //}

    //private IEnumerable TestCoroutine(Action Onstart = null, Action Oncomplete = null)
    //{
    //    OnStart?.Invoke();
    //    yield return new WaitForSeconds(1f);
    //    OnComplete?.Invoke();
    //}
    //void OnTestStart()
    //{
    //    Debug.Log("Start");
    //}
    //void OnTestComplete()
    //{
    //    Debug.Log("Complete");
    //}
   
}
