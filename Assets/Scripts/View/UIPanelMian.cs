using UnityEngine;
using System.Collections;

public class UIPanelMian : UIService.UI {

    public UIPanelMian(string name) : base(name) { }

    public override void OnCreat()
    {
        base.OnCreat();
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
        Debug.Log("UpDateUI" + da.Name);
        Debug.Log("UpDateUI" + da.Age);

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }
}
