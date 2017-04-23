using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{

    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    bool refreshing;
    // Use this for initialization
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (refreshing && spriteRenderer.color.a < 1)
        {
            gameObject.SetActive(true);
            Color c = spriteRenderer.color;
            c.a += Time.fixedDeltaTime;
            spriteRenderer.color = c;
            StartCoroutine(Wait(0.1f));
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
    }

    void Pick()
    {
        Globals.Gold++;
        Globals.OnMapComplete += RefreshCoin;
        /*Color c = spriteRenderer.color;
        while (spriteRenderer.color.a > 0)
        {
            c.a -= Time.deltaTime;
            spriteRenderer.color = c;
        }*/
        gameObject.SetActive(false);
        Debug.Log("Finish");
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
