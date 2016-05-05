using System;

public class Prop<T>
{
    Action<T> OnValueChange;
    T lastValue;

    public bool IsSend { get; set; }

    public T Value { private set; get; }

    public Prop()
    {
        Value = default(T);
        lastValue = default(T);
    }

    public void SetValue(T value, bool isChangeSend = true)
    {
        if (!value.Equals(lastValue))
        {
            this.Value = value;
            this.lastValue = value;
            if (IsSend) Invoke();
        }
        else
        {
            if (!isChangeSend)
            {
                if (IsSend) Invoke();
            }
        }
    }

    public void ClearValue()
    {
        Value = default(T);
    }

    public void AddListener(Action<T> onValueChange)
    {
        this.OnValueChange += onValueChange;
    }

    public void RemoveListener(Action<T> onValueChange)
    {
        this.OnValueChange -= onValueChange;
    }

    public void Invoke()
    {
        if (OnValueChange != null)
            OnValueChange(Value);
    }
}
