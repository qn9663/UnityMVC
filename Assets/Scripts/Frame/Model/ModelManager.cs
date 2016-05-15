using System;
using System.Collections.Generic;

namespace Frame
{
    public class ModelManager
    {
        static ModelManager _instance = null;
        public static ModelManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ModelManager();
                return _instance;
            }
        }

        Dictionary<string, ModelBase> _dataDic = new Dictionary<string, ModelBase>();

        public T CreatData<T>(string name) where T : ModelBase
        {
            T t;
            if (_dataDic.ContainsKey(name))
            {
                t = _dataDic[name] as T;
            }
            else
            {
                t = Activator.CreateInstance(typeof(T), name) as T;
                t.OnCreat();
                _dataDic.Add(name, t);
            }
            return t;
        }

        public T GetDataForm<T>(string name) where T : ModelBase
        {
            T t = CreatData<T>(name);
            t.mLocalData = t.GetDataFrom();
            t.OnUpdate();
            return t;
        }

        public ModelBase GetData(string name)
        {
            if (_dataDic.ContainsKey(name))
                return _dataDic[name];
            return null;
        }
    }
}