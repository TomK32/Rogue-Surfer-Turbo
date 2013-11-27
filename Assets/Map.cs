using UnityEngine;
using System.Collections;

public class Tile {
  public enum Types:byte {
    Ocean,
    Beach,
    Rock
  }
  public Vector3 position;
  public Color[] color;

  public Tile() {}

  public void setColor(Color c, int width, int height) {
    color = new Color[width * height];
    int i = 0;
    for (int x=0; x < width; x++) {
      for (int y=0; y < height; y++) {
        color[i] = c;
        i++;
      }
    }
  }
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

    tiles = new Tile[height, width];
	}
  public Tile GetTile(int x, int y) {
    return tiles[y,x];
  }
}
