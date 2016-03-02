using UnityEngine;
using System.Collections;
using System;

public class PanelMainCtr : State {

	// Use this for initialization
	void Start () {
        var ui = UIService.Instance.ShowUI<UIPanelMian>(ConstStrings.Panel_Main);
        Enter();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Enter()
    {
        NotificationCenter.instance.AddObserver(OnDataUpdate, ConstStrings.Event_panelMain, "Chang");
        base.Enter();
        var data = DataService.Instance.GetDataForm<DataPanelMain>(ConstStrings.Panel_Main);
       
    }

    private void OnDataUpdate(object arg1, object arg2)
    {
        PanelMainKeyValue data = (PanelMainKeyValue)arg2;
        UIService.Instance.UpDateUI(ConstStrings.Panel_Main, data);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
