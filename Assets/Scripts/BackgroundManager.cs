using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {
    public Sprite[] sprites;
    public float pulseTime = 3f;
    public SpriteRenderer[] backgrounds;
    private float accumulator;
    private int currentBG;
    private bool toTransparent;
    private int[] spriteValues;
	// Use this for initialization
	void Start () {
        accumulator = 0;
        currentBG = 0;
        toTransparent = true;
        spriteValues = new int[2] { 0, 1 };
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Pulse();
	}

    void Pulse()
    {
        accumulator += Time.fixedDeltaTime;
        Color c = backgrounds[0].color;
        c.a += (toTransparent ? -1 : 1) * Time.deltaTime / pulseTime;
        backgrounds[0].color = c;
        if (accumulator >= pulseTime)
        {
            accumulator -= pulseTime;
            spriteValues[currentBG] = (spriteValues[currentBG] + 1) % 4;
            backgrounds[currentBG].sprite = sprites[spriteValues[currentBG]];
            currentBG = ++currentBG % 2;
            toTransparent = !toTransparent;
        }
    }
}
