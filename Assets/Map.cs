using UnityEngine;
using System.Collections;

public class Tile {
  public enum Sorts:int {
    Ocean,
    Beach,
    Rock
  }
  public Vector3 position;
  public Color[] color;
  public int Sort = -1;
  public float depth = 0.0f; // 1..0 for the beach and 0..-1 for the sea

  public Tile() {}
  public string ColliderPrefabName = null;

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
  public Ocean() {
    this.Sort = (int)Tile.Sorts.Ocean;
  }
}

public class Beach : Tile {
  public Beach() {
    this.Sort = (int)Tile.Sorts.Beach;
  }
}

public class Rock : Tile {
  public Rock() {
    this.Sort = (int)Tile.Sorts.Rock;
    this.ColliderPrefabName = "Rock";
  }
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
