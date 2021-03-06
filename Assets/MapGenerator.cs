﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class MapGenerator : MonoBehaviour {

  // Map Generation Parameters
  private float sea_beach_ratio = 0.8f;
  public const int MAX_DEPTH = 10;
  private int tileSize = 16;

  public int seed;
  public Map map;

  public void Start() {
    this.seed = (int)GetUnixEpoch() % 1000;
    Random.seed = this.seed;
    BuildMap();
    BuildMesh();
    BuildColliders();
  }
  public void BuildMap() {
    int height = 28;
    int width = 48;

    map = new Map(width, height);
    //transform.position = new Vector3(transform.position.x, height, transform.position.z);
    Color c;
    // http://www.colourlovers.com/palette/2105064/sands_of_time
    Color colour_rock = new Color(86/256.0f, 112/256.0f, 126/256.0f, 1.0f);
    Color colour_ocean = new Color(97/256.0f, 166/256.0f, 171/256.0f, 1.0f);
    Color colour_sand = new Color(241/256.0f, 228/256.0f, 181/256.0f, 1.0f);

    for (int y = 0; y < height; y++) {
      float y_r = (float)y / height;
      for (int x = 0; x < width; x++) {
        float s_y = Mathf.PerlinNoise(seed + x/64.0f, seed + y_r*32.0f);
        float s_x = Mathf.PerlinNoise(seed + x*4.0f, seed + y_r/8.0f);
        float depth = (1 - y_r) - sea_beach_ratio + s_y/8;
        if (y_r < 0.8f && y_r > 0.6f && s_y > 0.3f && s_y < 0.4f) {
          // rocks
          map.tiles[y,x] = new Tile(Tile.Sorts.Rock);
          c = colour_rock * Random.Range(0.5f, 1.1f);
        }
        if (depth < 0) {
          // ocean
          map.tiles[y,x] = new Tile(Tile.Sorts.Ocean);
          c = colour_ocean * Random.Range(0.98f, 1.0f) * (1 - Mathf.Pow(-depth, 2.0f));
        } else {
          // beach
          map.tiles[y,x] = new Tile(Tile.Sorts.Beach);
          c = colour_sand * Random.Range(0.9f, 1.0f);
        }
        map.tiles[y,x].position = new Vector3(x, y, 0);
        map.tiles[y,x].setColor(c, width, height);
        map.tiles[y,x].depth = s_y;
      }
    }
  }

  public void BuildTexture() {
    Texture2D texture = new Texture2D(map.width * tileSize, map.height * tileSize);

    for(int y=0; y < map.height; y++) {
      for(int x=0; x < map.width; x++) {
        texture.SetPixels(x * tileSize, y * tileSize, tileSize, tileSize, map.GetTile(x,y).color);
      }
    }

    texture.filterMode = FilterMode.Point;
    texture.wrapMode = TextureWrapMode.Clamp;
    texture.Apply();

    MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
    mesh_renderer.sharedMaterials[0].mainTexture = texture;
  }

  public void BuildColliders() {
    GameObject colliderHolder = new GameObject("Map colliders");
    colliderHolder.transform.parent = transform;

    int x, y;
    for (y=0; y < map.height; y++) {
      for (x=0; x < map.width; x++) {
        string collider_name = map.GetTile(x, y).ColliderPrefabName;
        if (collider_name != null) {
          Vector3 position = new Vector3(x, y, 0);
          GameObject collider = (GameObject)Instantiate(Resources.Load(collider_name), position, Quaternion.identity);
          collider.transform.parent = colliderHolder.transform;
        }
      }
    }
  }

  // roughly following the TileMap tutorial of Quill18 http://quill18.com/unity_tutorials/
  public void BuildMesh() {
    Mesh mesh = new Mesh();
    int numVerts = (map.width + 1) * (map.height + 1);
    Vector3[] vertices = new Vector3[numVerts];
    Vector3[] normals = new Vector3[numVerts];
    Vector2[] uv = new Vector2[numVerts];
    int[] triangles = new int[map.width * map.height * 6];

    int x, y, i;
    i = 0;
    for (y=0; y <= map.height; y++) {
      for (x=0; x <= map.width; x++) {
        vertices[i] = new Vector3( x * 1.0f, - y * 1.0f, 0);
        normals[i] = Vector3.up;
        uv[i] = new Vector2( (float) x / map.width, 1.0f - (float) y / map.height );
        i++;
      }
    }

    // triangles
    i = 0;
    int i6 = 0;
    int v = 0;
    for (y=0; y < map.height; y++) {
      for (x=0; x < map.width; x++) {
        i6 = i * 6;
        triangles[i6 + 0] = v;
        triangles[i6 + 2] = v + map.width + 1;
        triangles[i6 + 1] = v + map.width + 2;

        triangles[i6 + 3] = v;
        triangles[i6 + 5] = v + map.width + 2;
        triangles[i6 + 4] = v + 1;
        i++;
        v++;
      }
      v++;
    }

    mesh.vertices = vertices;
    mesh.uv = uv;
    mesh.normals = normals;
    mesh.triangles = triangles;
    GetComponent<MeshFilter>().mesh = mesh;
    BuildTexture();
  }

  // http://stackoverflow.com/questions/906034/calculating-future-epoch-time-in-c-sharp
  private static double GetUnixEpoch()
  {
      var unixTime = System.DateTime.Now.ToUniversalTime() - 
          new System.DateTime(1970, 1, 1, 0, 0, 0);

      return unixTime.TotalSeconds;
  }

}
