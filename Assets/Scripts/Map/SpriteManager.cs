using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {
    public Sprite[] sprites;
    private static SpriteManager spriteManager;
    public static SpriteManager Instance
    {
        get
        {
            return spriteManager;
        }
    }
    void Start()
    {
        if (spriteManager == null)
        {
            spriteManager = this;
        }
    }
}
