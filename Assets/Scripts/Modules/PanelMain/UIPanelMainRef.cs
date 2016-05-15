using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIPanelMainRef : MonoBehaviour
{
    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Text text;

    public Button.ButtonClickedEvent OnClickButton1
    {
        get { return button1.onClick; }
    }

    public Button.ButtonClickedEvent OnClickButton2
    {
        get { return button2.onClick; }
    }

    public string textValue
    {
        set { text.text = value; }
    }
}
