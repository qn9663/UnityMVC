
public class DataPanelMain : DataService.Data
{
    public DataPanelMain(string name) : base(name) { }

    public override void OnCreat()
    {
        base.OnCreat();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        NotificationCenter.instance.PostNotification(EventString.Event_panelMain,"Chang", mLocalData);
    }

    public override KeyValueBase GetData()
    {
        var data = new PanelMainKeyValue();
        data.Name = "chang";
        data.Age = 24;
        return data;
    }
}
