using Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PanelMainView : ViewBase
{
    public UIPanelMainRef panelRef { get; private set; }
    public PanelMainView(string name) : base(name)
    {
    }

    public override void OnCreat()
    {
        base.OnCreat();
        panelRef = mPrefab.GetComponent<UIPanelMainRef>();
        panelRef.OnClickButton1.AddListener(() => SendClickMessage("OnClickButton1"));
        panelRef.OnClickButton2.AddListener(() => SendClickMessage("OnClickButton2"));
    }

    public override void OnShow(bool isShow)
    {
        base.OnShow(isShow);
    }

    public override void UpDateUI(KeyValueBase data)
    {
        var panelMainData = (PanelMainKV)(data);
        panelRef.textValue = panelMainData.Name;
    }

    public override void OnDestory()
    {
        base.OnDestory();
        panelRef.OnClickButton1.RemoveAllListeners();
        panelRef.OnClickButton2.RemoveAllListeners();
    }
}
