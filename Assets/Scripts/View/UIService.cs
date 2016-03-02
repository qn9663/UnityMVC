using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class UIService
{
    static UIService instance = null;
    public static UIService Instance
    {
        get
        {
            if (instance == null)
                instance = new UIService();
            
            return instance;
        }
    }

    Dictionary<string, UI> _uiDic = new Dictionary<string, UI>();

    public T CreatUI<T>(string name) where T : UI
    {
        T t = CreatItemUI<T>(name, name, null);
        return t;
    }

    public T ShowUI<T>(string name) where T : UI
    {
        T t = CreatUI<T>(name);
        t.OnShow(true);
        return t;
    }

    public T CreatItemUI<T>(string name, string uiKey, Transform parent) where T : UI
    {
        T t = null;
        if (_uiDic.ContainsKey(uiKey))
        {
            t = _uiDic[uiKey] as T;
        }
        else
        {
            t = Activator.CreateInstance(typeof(T), uiKey) as T;
            _uiDic.Add(uiKey, t);
            t.OnCreat();
        }
        return t;
    }

    public T ShowItemUI<T>(string name, string uiKey, Transform parent) where T : UI
    {
        T t = CreatItemUI<T>(name, uiKey, parent);
        t.OnShow(true);
        return t;
    }



    public void UpDateUI(UI ui, KeyValueBase data)
    {
        ui.UpDateUI(data);
    }

    public void UpDateUI(string uiKey, KeyValueBase data)
    {
        var ui = GetUI(uiKey);
        Debug.Assert(ui != null, "更新的UI为空");
        if (ui != null)
            ui.UpDateUI(data);
    }

    public UI GetUI(string name)
    {
        Debug.Assert(_uiDic.ContainsKey(name), "查找的UI不存在");
        if (_uiDic.ContainsKey(name))
            return _uiDic[name];
        return null;
    }

    public void CloseUI(string name, bool isDestroy)
    {
        Debug.Assert(_uiDic.ContainsKey(name), "要关闭的UI不存在");
        if (_uiDic.ContainsKey(name))
        {
            var ui = _uiDic[name];
            ui.OnShow(false);
            if (isDestroy)
            {
                ui.OnDestory();
            }
        }
    }

    public abstract class UI
    {
        public GameObject mPrefab { get; set; }

        public UI(string name) { }
        public virtual void OnCreat() { }
        public virtual void OnShow(bool isShow) { }
        //public abstract void OnClick(GameObject go);
        public virtual void UpDateUI(KeyValueBase data) { }
        public virtual void OnUpdate() { }
        public virtual void OnClose() { }
        public virtual void OnActive(bool isActive) { }
        public virtual void OnDestory() { }
    }
}
