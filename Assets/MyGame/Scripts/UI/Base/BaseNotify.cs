using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNotify : BaseUIElement
{
    public override void Init()
    {
        base.Init();
        this.uiType = UIType.NOTIFY;
    }

    public override void Show(object data)
    {
        base.Show(data);
    }
    public override void Hide()
    {
        base.Hide();
    }
    public override void OnClickBackButton()
    {
        base.OnClickBackButton();
    }



}
