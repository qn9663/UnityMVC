using System;

namespace Frame
{
    public class Prop<T>
    {
        public enum SendType
        {
            nomalSend,
            changeSend,
            dontSend
        }

        private SendType _sendType = SendType.nomalSend;
        public SendType mSendType
        {
            get { return _sendType; }
            set { _sendType = value; }
        }

        Action<T> OnValueChange;
        T lastValue;

        public T Value { private set; get; }

        public Prop()
        {
            Value = default(T);
            lastValue = default(T);
        }

        public void SetAndSendValue(T value)
        {
            switch (_sendType)
            {
                case SendType.nomalSend:
                    SetValue(value);
                    SendChange();
                    break;
                case SendType.changeSend:
                    if (value.Equals(lastValue)) break;
                    SetValue(value);
                    SendChange();
                    break;
                case SendType.dontSend:
                    SetValue(value);
                    break;
                default:
                    break;
            }
        }

        public void SetValue(T value)
        {
            Value = value;
            lastValue = value;
        }

        public void ClearValue()
        {
            Value = default(T);
            lastValue = default(T);
        }

        public void AddListener(Action<T> onValueChange)
        {
            this.OnValueChange += onValueChange;
        }

        public void RemoveListener(Action<T> onValueChange)
        {
            this.OnValueChange -= onValueChange;
        }

        public void SendChange()
        {
            if (OnValueChange != null)
                OnValueChange(Value);
        }
    }
}
