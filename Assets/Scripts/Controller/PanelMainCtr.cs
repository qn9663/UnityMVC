using UnityEngine;
using System.Collections;
using System;

public class PanelMainCtr : State
{

    Prop<int> prop_Num = new Prop<int>();

    UIPanelMian _uiPanelMian;
    // Use this for initialization
    void Start()
    {
        int num = 5;
        Debug.Log("SetValue之前 " + num);
        num.AddPropLisenter(OnNumChange);
        num.SetValue(6, out num);
        num.SetSendTypeToChange();
        num.SetValue(6, out num);
        Debug.Log("SetValue中 " + num);

        num.RemovePropLisenter(OnNumChange);
        Debug.Log("SetValue之后 " + num);
        Enter();
    }

    private void OnNumChange(object obj)
    {
        int num = (int)obj;
        Debug.Log(num);
    }

    private void OnValueChange1(object obj)
    {
        Transform tra = (Transform)obj;
        Debug.Log(tra.name);
    }

    private void OnValueChange(object obj)
    {
        Transform tra = (Transform)obj;
        Debug.Log(tra.name);
    }

    private void OnNumValueChange(int a)
    {
        Debug.Log(string.Format("value的值变化为{0}", a));
    }

    void OnClickPanelMain(object sender, object buttonId)
    {
        var id = (int)buttonId;
        switch (id)
        {
            case (int)PanelMianButtonIDEnum.Button_1:
                UIService.Instance.CloseUI(ConstStrings.Panel_Main);
                break;
            case (int)PanelMianButtonIDEnum.Button_2:
                Debug.Log("点击了按钮22222222");
                break;
            default:
                break;
        }
    }

    public override void Enter()
    {
        _uiPanelMian = UIService.Instance.ShowUI<UIPanelMian>(ConstStrings.Panel_Main);
        _uiPanelMian.AddListener(OnClickPanelMain);

        NotificationCenter.instance.AddObserver(OnDataUpdate, EventString.Event_panelMain, "Chang");
        base.Enter();
        DataService.Instance.GetDataForm<DataPanelMain>(ConstStrings.Panel_Main);
    }

    private void OnDataUpdate(object arg1, object arg2)
    {
        PanelMainKeyValue data = (PanelMainKeyValue)arg2;
        UIService.Instance.UpDateUI(ConstStrings.Panel_Main, data);
    }

    public override void Exit()
    {
        base.Exit();
        _uiPanelMian.RemoveListner(OnClickPanelMain);
        _uiPanelMian = null;
    }
}
