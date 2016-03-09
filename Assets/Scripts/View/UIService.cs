using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;


using Handler = System.Action<System.Object, System.Object>;

public class UIService
{

    Dictionary<string, UI> _uiDic = new Dictionary<string, UI>();
    UI _currentUI;
    Transform _canvas;

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
            var prefab = Resources.Load<GameObject>("UI/" + name);
            if (prefab != null)
            {
                var tempObj = GameObject.Instantiate<GameObject>(prefab);
                tempObj.transform.SetParent(parent);
                tempObj.transform.localPosition = Vector3.zero;
                tempObj.transform.localScale = Vector3.one;
                tempObj.SetActive(false);
                t.mPrefab = tempObj;
                _uiDic.Add(uiKey, t);
                t.OnCreat();
            }
            else
            {
                Debug.LogError("没有名字为" + name + "的预制件");
            }
        }
        return t;
    }

    public T CreatUI<T>(string name) where T : UI
    {
        if (_canvas == null) _canvas = GameObject.FindObjectOfType<Canvas>().transform;
        T t = CreatItemUI<T>(name, name, _canvas);
        EventTriggerListener.Get(EventSystem.current.gameObject).onClick = t.OnClick;
        return t;
    }

    public T ShowUI<T>(string name) where T : UI
    {
        T t = CreatUI<T>(name);
        _currentUI = t;
        t.mPrefab.SetActive(true);
        t.OnShow(true);
        return t;
    }

    public T ShowItemUI<T>(string name, string uiKey, Transform parent) where T : UI
    {
        T t = CreatItemUI<T>(name, uiKey, parent);
        t.mPrefab.SetActive(true);
        t.OnShow(true);
        return t;
    }

    public void UpDateUI(UI ui, KeyValueBase keyValue)
    {
        Debug.Assert(ui != null, "ui为null");
        Debug.Assert(keyValue != null, "keyValue == null");
        if (ui == null && keyValue == null) return;
        ui.UpDateUI(keyValue);
    }

    public void UpDateUI(string uiKey, KeyValueBase data)
    {
        var ui = GetUIBy(uiKey);
        Debug.Assert(ui != null, "更新的UI为空");
        if (ui != null)
            ui.UpDateUI(data);
    }

    public UI GetUIBy(string name)
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
            ui.Show(false);
            if (isDestroy)
            {
                GameObject.Destroy(ui.mPrefab);
                ui.mPrefab = null;
                ui.OnDestory();
                _uiDic.Remove(name);
            }
        }
    }

    public abstract class UI
    {
        public GameObject mPrefab { get; set; }

        public UI(string name) { }
        public virtual void OnCreat() { }
        public virtual void OnShow(bool isShow) { }
        public virtual void OnClick(GameObject go) { }
        public virtual void UpDateUI(KeyValueBase data) { }
        public virtual void OnUpdate() { }
        public virtual void OnClose() { }
        public virtual void OnActive(bool isActive) { }
        public virtual void OnDestory() { }

        public void Show(bool show)
        {
            mPrefab.SetActive(show);
            OnShow(show);
        }

        public void AddListener(Handler handler)
        {
            this.AddObserver(handler, EventString.Event_UI);
        }

        public void SendMessage(string buttonName)
        {
            this.PostNotification(EventString.Event_UI, buttonName);
        }

        public void RemoveListner(Handler handler)
        {
            this.RemoveObserver(handler, EventString.Event_UI);
        }
    }
}
