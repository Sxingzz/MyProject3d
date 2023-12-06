﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadCastEventTest : MonoBehaviour
{
    private int countVal = 0;

    private string strVal = "Hello";



    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            countVal++;

            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.LEFT_MOUSE_CLICK, countVal);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.RIGHT_MOUSE_CLICK, strVal);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return)) // nhấn enter
        {
            PlayerInfo playerInfo = new PlayerInfo()
            {
                PlayerName = "Sao",
                PlayerAge = 100
            };
            if (ListenerManager.HasInstance)
            {
                ListenerManager.Instance.BroadCast(ListenType.UPDATE_PLAYER_INFO, playerInfo);
            }
        }
    }
}
