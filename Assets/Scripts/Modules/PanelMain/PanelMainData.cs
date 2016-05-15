using Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PanelMainData : DataBase
{
    public PanelMainModel mainModel { get; private set; }
    public PanelMainData(string dataName) : base(dataName)
    {
        mainModel = ModelManager.Instance.GetDataForm<PanelMainModel>("PanelMainModel");
    }

    public override void ServiceToClient(int msyType, string mesage)
    {
        base.ServiceToClient(msyType, mesage);
    }
}
