using Frame;
using UnityEngine;

public class PanelMainController : ControllerBase
{
    public PanelMainController(string ctrName) : base(ctrName)
    {

    }

    public override void OnInitModule()
    {
        base.OnInitModule();
    }

    public override void OnModuleOpen()
    {
        base.OnModuleOpen();
        view = ShowUI<PanelMainView>("CanvasPanelMain", "PanelMain");
        view.SetCtr(this);
        var data = (PanelMainData)ModuleData;
        view.UpDateUI(data.mainModel.mLocalData);
    }

    public override void OnModuleClose()
    {
        base.OnModuleClose();
        view.RemoveCtr();
        DisableUI("PanelMain");
        view = null;
    }

    void OnClickButton1()
    {
        Debug.Log("点击了按钮1");
    }

    void OnClickButton2()
    {
        Debug.Log("点击了按钮2");
    }
}