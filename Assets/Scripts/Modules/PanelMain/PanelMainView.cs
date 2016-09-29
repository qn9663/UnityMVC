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
        panelRef.button1.onClick.AddListener(() => SendClickMessage(panelRef.button1));
        panelRef.button2.onClick.AddListener(() => SendClickMessage(panelRef.button2));
    }

    public override void OnShow(bool isShow)
    {
        base.OnShow(isShow);
    }

    public override void UpDateUI(KeyValueBase data)
    {
        var panelMainData = (PanelMainKV)(data);
        panelRef.text.text = panelMainData.Name;
    }

    public override void OnDestory()
    {
        base.OnDestory();
        panelRef.button1.onClick.RemoveAllListeners();
        panelRef.button2.onClick.RemoveAllListeners();
    }
}
