using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TileEventArgs : EventArgs
{
    public enum TileSet
    {
        Castle,
        Dirt,
        Grass,
        Sand,
        Snow
    }
    public TileSet type;
}
