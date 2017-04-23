using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCounter : MonoBehaviour {
    public Sprite[] numbers;
    public Image[] signs;
    void Awake()
    {
        Globals.OnGoldAmountChanged += UpdateCounter;
    }
    void Start()
    {
        signs[0].sprite = numbers[0];
        signs[1].sprite = numbers[0];
    }
    void UpdateCounter(object sender, EventArgs e)
    {
        int gold = Globals.Gold;
        int units = gold % 10;
        int dozens = (gold % 100) / 10;
        signs[0].sprite = numbers[units];
        signs[1].sprite = numbers[dozens];
    }
}
