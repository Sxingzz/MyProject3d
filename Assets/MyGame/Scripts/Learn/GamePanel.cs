using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePanel : MonoBehaviour
{
    // dùng listener manager phải lắng nghe trong hàm start hoặc hủy lắng nghe trong hàm Ondestroy
    // nếu lắng nghe trong hàm Onenable thì hủy lắng nghe trong hàm OnDisable

    public TextMeshProUGUI testText;
    public TextMeshProUGUI strText;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI AgeText;

    private void Start()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.LEFT_MOUSE_CLICK, OnListenLeftMouseClickEvent);
            ListenerManager.Instance.Register(ListenType.RIGHT_MOUSE_CLICK,OnListenRightMouseClickEvent);
            ListenerManager.Instance.Register(ListenType.UPDATE_PLAYER_INFO,OnUpdatePlayerInfoEvent);
        }
    }

    private void OnDestroy()
    {
        ListenerManager.Instance.Unregister(ListenType.LEFT_MOUSE_CLICK, OnListenLeftMouseClickEvent);
        ListenerManager.Instance.Unregister(ListenType.RIGHT_MOUSE_CLICK,OnListenRightMouseClickEvent);
        ListenerManager.Instance.Unregister(ListenType.UPDATE_PLAYER_INFO,OnUpdatePlayerInfoEvent);
    }

    private void OnListenLeftMouseClickEvent(object value)
    {
        if(value != null)
        {
            if(value is int countValue)
            {
                testText.text = countValue.ToString();
            }
        }
    }
    private void OnListenRightMouseClickEvent(object value)
    {
        if(value != null)
        {
            if (value is string stringValue)
            {
                strText.text = stringValue;
            }
        }
    }
    private void OnUpdatePlayerInfoEvent(object value)
    {
        if(value != null)
        {
            if(value is PlayerInfo playerInfo)
            {
                NameText.text = playerInfo.PlayerName;
                AgeText.text = playerInfo.PlayerAge.ToString();
            }
        }
    }
}
