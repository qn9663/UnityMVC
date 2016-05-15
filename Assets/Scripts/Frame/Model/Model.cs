namespace Frame
{
    public abstract class ModelBase
    {
        public KeyValueBase mLocalData { get; set; }

        public ModelBase(string name) { }
        public virtual KeyValueBase GetDataFrom() { return mLocalData; }
        public virtual void OnCreat() { }
        public virtual void OnUpdate() { }
        public virtual void OnDestory() { }
    }
}