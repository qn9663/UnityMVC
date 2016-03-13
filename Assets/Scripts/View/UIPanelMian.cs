using UnityEngine;
using System.Collections.Generic;

public enum PanelMianButtonIDEnum
{
    Button_1 = 1,
    Button_2 = 2,
}

public class UIPanelMian : UIService.UI
{
    RefPanelMian _refPanelMain;
    public UIPanelMian(string name) : base(name) { }

    public override void OnCreat()
    {
        base.OnCreat();
        _refPanelMain = mPrefab.GetComponent<RefPanelMian>();
    }

    public override void OnShow(bool isShow)
    {
        base.OnShow(isShow);
        if (isShow) { }
        else { }
    }

    public override void UpDateUI(KeyValueBase data)
    {
        base.UpDateUI(data);
        var da = (PanelMainKeyValue)data;
        _refPanelMain.mText = "名字是：" + da.Name + "年龄是： " + da.Age;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
