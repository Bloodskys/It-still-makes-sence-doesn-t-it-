using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    bool refreshing;
    bool depleting;
    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (refreshing && spriteRenderer.color.a < 1)
        {
            Color c = spriteRenderer.color;
            c.a += Time.fixedDeltaTime * 2;
            spriteRenderer.color = c;
            if (c.a >= 1)
            {
                refreshing = false;
            }
        }
        else if (depleting && spriteRenderer.color.a > 0)
        {
            Color c = spriteRenderer.color;
            c.a -= Time.fixedDeltaTime * 3;
            spriteRenderer.gameObject.transform.localScale *= (1 + Time.fixedDeltaTime * 2);
            spriteRenderer.color = c;
            if (c.a <= 0)
            {
                depleting = false;
                spriteRenderer.gameObject.transform.localScale = Vector3.one;
                gameObject.SetActive(false);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Pick();
        }
    }

    void RefreshCoin(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        refreshing = true;
        depleting = false;
    }

    void Pick()
    {
        Globals.Gold++;
        Globals.OnMapComplete += RefreshCoin;
        depleting = true;
        refreshing = false;
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
