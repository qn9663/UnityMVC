
namespace Frame
{
    public abstract class DataBase
    {
        public string dataName { private set; get; }
        public DataBase(string dataName)
        {
            this.dataName = dataName;
        }

        public bool IsRecive { get; set; }

        public virtual void ServiceToClient(int msyType,string mesage) { }

        public void ClientToService() { }

        public virtual void OnModuleClose() { }
    }
}
