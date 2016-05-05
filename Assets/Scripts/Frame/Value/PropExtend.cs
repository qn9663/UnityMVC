using System;
using System.Collections.Generic;

public static class PropExtend
{
    public static Dictionary<string, Prop<object>> propSysDic = new Dictionary<string, Prop<object>>();
    public static Dictionary<object, string> objectNameDic = new Dictionary<object, string>();

    public static void AddPropLisenter(this object obj, Action<object> onChange)
    {
        Prop<object> propObj = GetProp(obj);
        propObj.SetValue(obj);
        propObj.AddListener(onChange);
    }

    public static void RemovePropLisenter(this object obj, Action<object> onChange)
    {
        Prop<object> propObj = GetProp(obj);
        propObj.RemoveListener(onChange);
    }

    public static void SetValue<T>(this object obj, T value, out T newValue)
    {
        Prop<object> propObj = GetProp(obj);
        propObj.SetAndSendValue(value);
        newValue = value;
    }

    public static void SetSendTypeToNomal(this object obj)
    {
        Prop<object> propObj = GetProp(obj);
        propObj.mSendType = Prop<object>.SendType.nomalSend;
    }

    public static void SetSendTypeToChange(this object obj)
    {
        Prop<object> propObj = GetProp(obj);
        propObj.mSendType = Prop<object>.SendType.changeSend;
    }

    public static void SetSendTypeToDont(this object obj)
    {
        Prop<object> propObj = GetProp(obj);
        propObj.mSendType = Prop<object>.SendType.dontSend;
    }

    private static string GetPropName(object obj)
    {
        string propName = "";
        if (!objectNameDic.ContainsKey(obj))
        {
            propName = Guid.NewGuid().ToString();
            objectNameDic.Add(obj, propName);
        }
        else
        {
            propName = objectNameDic[obj];
        }
        return propName;
    }

    private static Prop<object> GetProp(object obj)
    {
        string propName = GetPropName(obj);
        Prop<object> propObj;
        if (!propSysDic.ContainsKey(propName))
        {
            propObj = new Prop<object>();
            propSysDic.Add(propName, propObj);
        }
        else
        {
            propObj = propSysDic[propName];
        }

        return propObj;
    }
}
