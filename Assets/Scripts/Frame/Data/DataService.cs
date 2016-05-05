using System;
using System.Collections.Generic;

public class DataService
{
    static DataService _instance = null;
    public static DataService Instance
    {
        get
        {
            if (_instance == null)
                _instance = new DataService();
            return _instance;
        }
    }

    Dictionary<string, Data> _dataDic = new Dictionary<string, Data>();

    public T CreatData<T>(string name) where T : Data
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

    public T GetDataForm<T>(string name) where T : Data
    {
        T t = CreatData<T>(name);
        t.mLocalData = t.GetData();
        t.OnUpdate();
        return t;
    }

    public Data GetData(string name)
    {
        if (_dataDic.ContainsKey(name))
            return _dataDic[name];
        return null;
    }

    public abstract class Data
    {
        public KeyValueBase mLocalData { get; set; }

        public Data(string name) { }
        public virtual KeyValueBase GetData() { return mLocalData; }
        public virtual void OnCreat() { }
        public virtual void OnUpdate() { }
        public virtual void OnDestory() { }
    }
}