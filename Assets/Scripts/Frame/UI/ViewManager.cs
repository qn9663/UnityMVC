using UnityEngine;
using System.Collections.Generic;
using System;

namespace Frame
{
    public class ViewManager
    {
        Dictionary<string, ViewBase> uiDic = new Dictionary<string, ViewBase>();

        static ViewManager instance = null;
        public static ViewManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ViewManager();
                return instance;
            }
        }

        GameObject _CreatItemUI(GameObject prefab, Transform parent)
        {
            GameObject tempObj = prefab;
            Debug.Assert(parent == null, "父物体为null");
            if (parent == null) return tempObj;
            tempObj.transform.SetParent(parent);
            tempObj.transform.localPosition = Vector3.zero;
            tempObj.transform.localScale = Vector3.one;
            tempObj.SetActive(false);
            return tempObj;
        }

        public T CreatItemUI<T>(GameObject prefab, string name, Transform parent) where T : ViewBase
        {
            T t = null;
            if (uiDic.ContainsKey(name))
            {
                t = uiDic[name] as T;
            }
            else
            {
                t = Activator.CreateInstance(typeof(T), name) as T;
                var tempObj = _CreatItemUI(prefab, parent);
                if (tempObj != null)
                {
                    tempObj.name = name;
                    t.mPrefab = tempObj;
                    uiDic.Add(name, t);
                    t.OnCreat();
                }
            }
            return t;
        }

        public T ShowItemUI<T>(GameObject prefab, string name, Transform parent) where T : ViewBase
        {
            T t = CreatItemUI<T>(prefab, name, parent);
            t.mPrefab.SetActive(true);
            t.OnShow(true);
            return t;
        }

        public T CreatUI<T>(GameObject prefab, string name) where T : ViewBase
        {
            T t = CreatItemUI<T>(prefab, name, null);
            return t;
        }

        public T ShowUI<T>(GameObject prefab, string name) where T : ViewBase
        {
            T t = CreatItemUI<T>(prefab, name, null);
            t.mPrefab.SetActive(true);
            t.OnShow(true);
            return t;
        }

        public void UpDateUI(ViewBase ui, KeyValueBase keyValue)
        {
            Debug.Assert(ui != null, "ui为null");
            Debug.Assert(keyValue != null, "keyValue == null");
            if (ui == null && keyValue == null) return;
            ui.UpDateUI(keyValue);
        }

        public ViewBase GetUIBy<T>(string name) where T : ViewBase
        {
            //Debug.Assert(uiDic.ContainsKey(name), "查找的UI不存在");
            if (uiDic.ContainsKey(name))
                return uiDic[name] as T;
            return null;
        }

        public void CloseUI(string name, bool isDestroy = false)
        {
            Debug.Assert(uiDic.ContainsKey(name), "要关闭的UI不存在");
            if (uiDic.ContainsKey(name))
            {
                var ui = uiDic[name];
                ui.Show(false);
                if (isDestroy)
                {
                    ui.mPrefab = null;
                    ui.OnDestory();
                    uiDic.Remove(name);
                }
            }
        }

        public void CloseAllUI(bool isDestory = false)
        {
            foreach (var ui in uiDic)
            {
                CloseUI(ui.Key, isDestory);
            }
            uiDic.Clear();
        }
    }
}