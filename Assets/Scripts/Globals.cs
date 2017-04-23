using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {
    public Sprite[] sprites;
    static int gold;
    public static EventHandler OnGoldAmountChanged;
    public static EventHandler OnMapComplete;
    public static int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            if (value > 99)
            {
                value = 99;
            }
            gold = value;
            RaiseGoldAmountChanged(null, null);
        }
    }
    public static void RaiseGoldAmountChanged(object sender, EventArgs e)
    {
        if (OnGoldAmountChanged != null)
        {
            OnGoldAmountChanged.Invoke(sender, e);
        }
    }
    public static void RaiseMapComplete(object sender, EventArgs e)
    {
        if (OnMapComplete != null)
        {
            OnMapComplete.Invoke(sender, e);
        }
    }
	void Start () {
        Gold = 0;
	}
}
