using Frame;

public class PanelMainModel : ModelBase
{
    public PanelMainModel(string name) : base(name)
    {
    }

    public override KeyValueBase GetDataFrom()
    {
        var kv = new PanelMainKV();
        kv.Name = "Chang";
        return kv;
    }
}
