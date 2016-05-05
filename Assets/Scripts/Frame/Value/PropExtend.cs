using System;
using System.Collections.Generic;

public static class PropExtend
{
    public static Dictionary<string, Prop<object>> propSysDic = new Dictionary<string, Prop<object>>();

    public static void AddPropLisenter(this object obj, string propName, Action<object> onChange)
    {
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

        propObj.IsSend = false;
        propObj.SetValue(obj);
        propObj.AddListener(onChange);
    }

    public static void RemovePropLisenter(this object obj, string propName, Action<object> onChange)
    {
        if (propSysDic.ContainsKey(propName))
        {
            Prop<object> propObj = propSysDic[propName];
            propObj.RemoveListener(onChange);
        }
    }

    public static void SetValue(this object obj, string propName, object value, bool isChangeSend = true)
    {
        if (propSysDic.ContainsKey(propName))
        {
            Prop<object> propObj = propSysDic[propName];
            propObj.IsSend = true;
            propObj.SetValue(value, isChangeSend);
        }
    }

    public static object GetValue(this object obj, string propName, object value, bool isChangeSend = true)
    {
        if (propSysDic.ContainsKey(propName))
        {
            Prop<object> propObj = propSysDic[propName];
            return propObj.Value;
        }
        return new object();
    }
}
