using UnityEngine;
using System.Collections.Generic;

public class UIPanelMian : UIService.UI {
    public enum ButtonId
    {
        button_1 = 1,
    }
    Dictionary<string, string> _buttonDic = new Dictionary<string, string>();
    RefPanelMian _refPanelMain;
    public UIPanelMian(string name) : base(name) { }

    public override void OnCreat()
    {
        base.OnCreat();
        _buttonDic.Add("Button", ButtonId.button_1.ToString());
        _refPanelMain = mPrefab.GetComponent<RefPanelMian>();
    }

    public override void OnShow(bool isShow)
    {
        base.OnShow(isShow);
        if (isShow) { }
        else { }
    }

    public override void OnClick(GameObject go)
    {
        base.OnClick(go);
        var name = go.name;
        if (_buttonDic.ContainsKey(name))
            SendMessage(_buttonDic[name]);
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
