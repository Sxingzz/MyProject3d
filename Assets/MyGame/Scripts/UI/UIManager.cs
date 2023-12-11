using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager<UIManager> 
{
    public GameObject cScreen, cPopup, cNotify, cOverlap;

    private Dictionary<string, BaseScreen> screens = new Dictionary<string, BaseScreen>();
    private Dictionary<string, BasePopup> popups = new Dictionary<string, BasePopup>();
    private Dictionary<string, BaseNotify> notifies = new Dictionary<string, BaseNotify>();
    private Dictionary<string, BaseOverlap> overlaps = new Dictionary<string, BaseOverlap>();

    public Dictionary<string, BaseScreen> Screens => screens;
    public Dictionary<string, BasePopup > Popups => popups;
    public Dictionary<string, BaseNotify > Notifies => notifies;
    public Dictionary<string, BaseOverlap > Overlaps => overlaps;

    private BaseScreen curScreen;
    private BasePopup curPopup;
    private BaseNotify curNotify;
    private BaseOverlap curOverlap;

    public BaseScreen CurScreen => curScreen;
    public BasePopup CurPopup => curPopup;
    public BaseNotify CurNotify => curNotify;
    public BaseOverlap CurOverlap => curOverlap;

    private const string SCREEN_RESOURCES_PATH = "Prefabs/UI/Screen/";
    private const string POPUP_RESOURCES_PATH = "Prefabs/UI/Popup/";
    private const string NOTIFY_RESOURCES_PATH = "Prefabs/UI/Notify/";
    private const string OVERLAP_RESOURCES_PATH = "Prefabs/UI/Overlap/";

    private List<string> rmScreens = new List<string>();
    private List<string> rmPopups = new List<string>();
    private List<string> rmNotifies = new List<string>();
    private List<string> rmOverlaps = new List<string>();

    private GameObject GetUIPrefab(UIType type, string uiName)
    {
        GameObject result = null;
        var defaultPath = "";
        if (result == null)
        {
            switch (type)
            {
                case UIType.SCREEN:
                    {
                        defaultPath = SCREEN_RESOURCES_PATH + uiName;
                    }
                    break;
                case UIType.POPUP:
                    {
                        defaultPath = POPUP_RESOURCES_PATH + uiName;
                    }
                    break;
                case UIType.NOTIFY:
                    {
                        defaultPath = NOTIFY_RESOURCES_PATH + uiName;
                    }
                    break;
                case UIType.OVERLAP:
                    {
                        defaultPath = OVERLAP_RESOURCES_PATH + uiName;
                    }
                    break;
            }
            result = Resources.Load(defaultPath) as GameObject;
        }
        return result;
    }
}
