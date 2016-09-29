using UnityEngine;

namespace Frame
{
    public abstract class ControllerBase
    {
        public DataBase ModuleData { set; get; }
        public ViewBase view { set; get; }

        public string CtrName { private set; get; }

        public ControllerBase(string ctrName)
        {
            this.CtrName = ctrName;
        }

        protected GameObject GetGameObject(string prefabName)
        {
            return Object.Instantiate((GameObject)Resources.Load("UI/" + prefabName));
        }

        protected void PuchBackGameObject(GameObject obj)
        {
            Object.Destroy(obj);
        }

        protected T ShowUI<T>(string prefabName, string uiName) where T : ViewBase
        {
            GameObject prefab = null;
            var ui = GetUI<T>(uiName);
            if (ui != null) prefab = ui.mPrefab;
            if (prefab == null) prefab = GetGameObject(prefabName);
            return ViewManager.Instance.ShowUI<T>(prefab, uiName);
        }

        protected T GetUI<T>(string uiName) where T : ViewBase
        {
            return ViewManager.Instance.GetUIBy<T>(uiName) as T;
        }

        protected T ShowItemUI<T>(string prefabName, string uiName, Transform parent) where T : ViewBase
        {
            GameObject prefab = null;
            var ui = GetUI<T>(uiName);
            if (ui != null) prefab = ui.mPrefab;
            if (prefab == null) prefab = GetGameObject(prefabName);
            return ViewManager.Instance.ShowItemUI<T>(prefab, uiName, parent);
        }

        protected void DisableUI(string uiName)
        {
            ViewManager.Instance.CloseUI(uiName);
        }

        protected void DestoryUI(string uiName)
        {
            ViewManager.Instance.CloseUI(uiName, true);
        }

        public virtual void OnModuleOpen() { }

        public virtual void OnModuleClose() { }

        public virtual void OnInitModule() { }

        public virtual void OnClick(object obj) { }
    }
}
