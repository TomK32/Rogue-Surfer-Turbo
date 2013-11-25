using UnityEngine;
using System.Collections;

public class Tile {
  public enum Types:byte {
    Ocean,
    Beach,
    Rock
  }
  public Vector3 position;
    
  public Tile() {}
}

public class Ocean : Tile {
  public const byte Type = (byte)Tile.Types.Ocean;
}

public class Beach : Tile {
  public const byte Type = (byte)Tile.Types.Beach;
}

public class Map {
  public Tile[,] tiles;
  public int width;
  public int height;

	public Map(int w, int h) {
    width = w;
    height = h;

    tiles = new Tile[width,height];
	}
}
