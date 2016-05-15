using System;
using System.Reflection;
using UnityEngine;

namespace Frame
{
    public abstract class ViewBase
    {
        private ControllerBase Ctr;
        public GameObject mPrefab { get; set; }
        public ViewBase(string name) { }

        public virtual void OnCreat() { }
        public virtual void OnShow(bool isShow) { }
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

        public void SetCtr(ControllerBase ctr)
        {
            this.Ctr = ctr;
        }

        public void SendClickMessage(string methodName, params object[] param)
        {
            Type type = Ctr.GetType();
            BindingFlags flag = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static|BindingFlags.Instance;
            MethodInfo method = type.GetMethod(methodName,flag);
            Debug.Assert(method != null, "名字为,Ctr" + Ctr.CtrName + "的控制器没有" + methodName + "方法");
            if (method == null) return;
            object[] objs = param;
            method.Invoke(Ctr, objs);
        }

        public void RemoveCtr()
        {
            this.Ctr = null;
        }
    }
}