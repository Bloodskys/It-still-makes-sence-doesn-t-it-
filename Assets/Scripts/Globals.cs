using System;
using UnityEngine;

public class Globals : MonoBehaviour {
    public Sprite[] sprites;
    static int gold;
    public static EventHandler OnGoldAmountChanged;
    public static EventHandler OnMapComplete;
    public static EventHandler OnChangeTileset;
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
            RaiseChangeTileset(sender, e);
        }
    }
    public static void RaiseChangeTileset(object sender, EventArgs e)
    {
        TileEventArgs args = new TileEventArgs()
        {
            type = (TileEventArgs.TileSet)UnityEngine.Random.Range(0, 4)
        };
        if (OnChangeTileset != null)
        {
            OnChangeTileset.Invoke(sender, args);
        }
    }
    void Start () {
        Gold = 0;
	}
}
