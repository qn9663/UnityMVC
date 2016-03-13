using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RefPanelMian : UIService.RefUI
{
    [SerializeField]
    Image image;
    [SerializeField]
    Text text;

    public Sprite mSprite
    {
        get { return image.sprite; }
        set
        {
            if (value == null)
            {
                image.gameObject.SetActive(false);
            }
            else
            {
                image.gameObject.SetActive(true);
                image.sprite = value;
            }
        }
    }


    public string mText
    {
        get { return text.text; }
        set
        {
            text.text = value;
        }
    }
}
