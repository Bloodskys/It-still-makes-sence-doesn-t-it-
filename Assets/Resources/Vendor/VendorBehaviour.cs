using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendorBehaviour : MonoBehaviour {
    public GameObject dialogCloud;
    public string[] replies = new string[]
    {
        "Glad to see ya again!",
        "It still makes sense, doesn't it?",
        "You almost done.. nothing!"
    };
    private string notEnoughMoney = "You dont have enough money to get my *help*, try again.";
    private string enoughMoney = "Well done! Let's exchange!";
    private int lastReply;
    private int price;
    private float freezeTime;
    private float halfFreezeTime;
    private bool dialogRefreshed = true;
	// Use this for initialization
	void Start () {
        lastReply = 0;
        price = 20;
        Globals.OnMapComplete += RefreshDialog;
	}
	void RefreshDialog(object sender, EventArgs e)
    {
        dialogRefreshed = true;
    }
	// Update is called once per frame
	void Update () {
		if (dialogRefreshed && PlayerController.Player.transform.position.x > - 11)
        {
            ShowDialog(Globals.Gold >= price);
        }
        if (!dialogRefreshed && PlayerController.Player.transform.position.x > 1)
        {
            HideDialog();
        }

        //To freeze time near the Vendor (to read the message)
        /*if (freezeTime > 0)
        {
            Time.timeScale -= (freezeTime > halfFreezeTime?1:-1) * Time.unscaledDeltaTime;
            freezeTime -= Time.unscaledDeltaTime;
            if (freezeTime <= 0)
            {
                Time.timeScale = 1;
            }
        }*/
	}
    public void HideDialog()
    {
        dialogCloud.SetActive(false);
    }
    public void ShowDialog(bool canPay)
    {
        string reply = canPay ? enoughMoney : notEnoughMoney;
        if (canPay)
        {
            Globals.Gold -= price;
        }
        dialogCloud.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = reply;
        dialogCloud.SetActive(true);
        freezeTime = 1.5f;
        halfFreezeTime = freezeTime / 2;
        dialogRefreshed = false;
    }
}
