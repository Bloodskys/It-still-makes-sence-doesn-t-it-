using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    public enum TileType
    {
        Left,
        Mid,
        Right,
        Fill
    }
    public TileType type;
    void Start () {
        Globals.OnChangeTileset += ChangeTileset;
	}

    void ChangeTileset(object sender, EventArgs e)
    {
        GetComponent<SpriteRenderer>().sprite = SpriteManager.Instance.sprites[GetSpriteNumberFromType((int)((TileEventArgs)e).type, (int)type)];
    }

    int GetSpriteNumberFromType(int tileSet, int tileType)
    {
        return tileSet * 4 + tileType;
    }
}
