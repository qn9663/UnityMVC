using UnityEngine;
using System.Collections.Generic;
using System;

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
            var tempObj = _CreatItemUI(name, parent);
            if (tempObj != null)
            {
                tempObj.name = uiKey;
                t.mPrefab = tempObj;
                _uiDic.Add(uiKey, t);
                t.OnCreat();
            }
        }
        return t;
    }

    GameObject _CreatItemUI(string name, Transform parent)
    {
        GameObject tempObj = null;
        var prefab = Resources.Load<GameObject>("UI/" + name);
        if (prefab != null)
        {
            tempObj = GameObject.Instantiate<GameObject>(prefab);
            if (parent == null) return tempObj;
        }
        else
        {
            Debug.LogError("没有名字为" + name + "的预制件");
            return null;
        }
        tempObj.transform.SetParent(parent);
        tempObj.transform.localPosition = Vector3.zero;
        tempObj.transform.localScale = Vector3.one;
        tempObj.SetActive(false);
        return tempObj;
    }

    public T CreatUI<T>(string name) where T : UI
    {
        T t = CreatItemUI<T>(name, name, null);
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

    public void CloseUI(string name, bool isDestroy = false)
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

    public void CloseAllUI(bool isDestory = false)
    {
        foreach (var ui in _uiDic)
        {
            CloseUI(ui.Key, isDestory);
        }
        _uiDic.Clear();
    }

    public abstract class UI
    {
        public GameObject mPrefab { get; set; }

        public UI(string name) { }
        public virtual void OnCreat() { }
        public virtual void OnShow(bool isShow) { }
        public virtual void UpDateUI(KeyValueBase data) { }
        public virtual void OnUpdate() { }
        public virtual void OnClose() { }
        public virtual void OnActive(bool isActive) { }
        public virtual void OnDestory() { }

        public virtual void OnClick()
        {
            //var refUI = mPrefab.GetComponent<RefUI>();
            //Debug.Assert(refUI != null, "refUI == null");
            //if (refUI == null) return;

            //refUI.Buttons.ForEach(item =>
            //{
            //    if (item != null)
            //    {
            //        EventTriggerListener.Get(item.button).onClick = (g) =>
            //        {
            //            if (g.name == item.button.name)
            //                SendMessage(item.id);
            //        };
            //    }
            //});
        }

        public void Show(bool show)
        {
            mPrefab.SetActive(show);
            OnShow(show);
        }

        public void AddListener(Handler handler)
        {
            this.AddObserver(handler, EventString.Event_UI);
        }

        public void SendMessage(int buttonID)
        {
            this.PostNotification(EventString.Event_UI, buttonID);
        }

        public void RemoveListner(Handler handler)
        {
            this.RemoveObserver(handler, EventString.Event_UI);
        }
    }

    [Serializable]
    public class ButtonId
    {
        public GameObject button;
        public int id;
    }

    public abstract class RefUI : MonoBehaviour
    {
        [SerializeField]
        List<ButtonId> buttons;

        public List<ButtonId> Buttons { get { return buttons; } }
    }
}
