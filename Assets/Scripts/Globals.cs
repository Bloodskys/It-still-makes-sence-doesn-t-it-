using System;
using System.Collections;
using UnityEngine;

public class Globals : MonoBehaviour {
    public Sprite[] sprites;
    static int gold;
    public static EventHandler OnGoldAmountChanged;
    public static EventHandler OnMapComplete;
    public static EventHandler OnChangeTileset;
    public static Globals globals;
    public static Globals Instance
    {
        get
        {
            return globals;
        }
    }
    private float shakeCam;
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
        if (globals == null)
        {
            globals = this;
        }
        Gold = 0;
	}
    void Update()
    {
        if (shakeCam > 0)
        {
            Camera.main.transform.position = new Vector3(0,0,-10) + new Vector3(UnityEngine.Random.Range(0f, 0.1f), UnityEngine.Random.Range(0f, 0.1f));
            shakeCam -= Time.fixedDeltaTime;
            if (shakeCam <= 0)
            {
                Camera.main.transform.position = new Vector3(0, 0, -10);
            }
        }
    }
    public void ShakeCam(float seconds)
    {
        shakeCam = seconds;
        StartCoroutine(Wait(0.3f));
    }
    IEnumerator Wait(float s)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(s);
        Time.timeScale = 1;
    }
}
