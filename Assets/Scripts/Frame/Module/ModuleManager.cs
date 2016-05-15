using System;
using System.Collections.Generic;

namespace Frame
{
    public class ModuleManager
    {
        private static ModuleManager instance = null;
        public static ModuleManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ModuleManager();
                return instance;
            }
        }

        private Dictionary<string, ModuleBase> modules = new Dictionary<string, ModuleBase>();

        public void OpenMoudule<M, C, D>(string moduleName)
            where M : ModuleBase
            where C : ControllerBase
            where D : DataBase
        {
            ModuleBase module = GetModule(moduleName);
            if (module == null)
            {
                module = Activator.CreateInstance(typeof(M), moduleName) as M;
                if (module.Ctrl == null)
                {
                    module.Ctrl = Activator.CreateInstance(typeof(C), moduleName) as C;
                    module.Ctrl.ModuleData = Activator.CreateInstance(typeof(D), moduleName) as D;
                }
                module.OpenModule();
            }
            module.InitModule();
        }

        public void OpenMoudule<M, C>(string moduleName)
            where M : ModuleBase
            where C : ControllerBase
        {
            ModuleBase module = GetModule(moduleName);
            if (module == null)
            {
                module = Activator.CreateInstance(typeof(M), moduleName) as M;
                modules.Add(moduleName, module);
                module.OpenModule();
            }
            module.InitModule();
            if (module.Ctrl == null)
                module.Ctrl = Activator.CreateInstance(typeof(C), moduleName) as C;
        }

        public ModuleBase GetModule(string moduleName)
        {
            ModuleBase module = null;
            if (modules.ContainsKey(moduleName))
                module = modules[moduleName];
            return module;
        }

        public void CloseModule(string moduleName)
        {
            ModuleBase module = GetModule(moduleName);
            if (module == null) return;
            module.CloseModule();
        }
    }
}
