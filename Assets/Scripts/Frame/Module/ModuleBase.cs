namespace Frame
{
    public abstract class ModuleBase
    {
        string moduleName;

        public ControllerBase Ctrl { set; get; }
        public bool isAcitive { get; set; }

        public ModuleBase(string moduleName)
        {
            this.moduleName = moduleName;
        }

        public void OpenModule()
        {
            OnModuleOpen();
        }

        public void CloseModule()
        {
            OnModuleClose();
        }

        public void InitModule()
        {
            //ControllerManager.Instance.CloseController(ctrName);
            OnInitModule();
        }
        public virtual void OnModuleOpen()
        {
            Ctrl.OnModuleOpen();
        }
        public virtual void OnModuleClose()
        {
            Ctrl.OnModuleClose();
        }

        public virtual void OnInitModule() { }
    }
}
