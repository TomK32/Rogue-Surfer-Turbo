using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class MapGenerator : MonoBehaviour {

  // Map Generation Parameters
  public const int MAX_DEPTH = 10;
  public int tileSize = 16;

  public int seed;
  public Map map;

  void Start() {
    BuildMap();
    BuildMesh();
  }

  public void BuildMap() {
    int width = (int)Screen.width / 8;
    int height = (int)Screen.height / 8;
    float depth = 0.0f;

    map = new Map(width, height);
    Color c;

    for (int x = 0; x < width; x++) {
      for (int y = 0; y < height; y++) {
        if (y > height * 0.2) {
          map.tiles[x,y] = new Ocean();
          depth = y * - MAX_DEPTH;
          c = new Color(0, Random.Range(0, 0.1f), Random.Range(0.9f, 1), 1.0f);
        } else {
          map.tiles[x,y] = new Beach();
          depth = x * MAX_DEPTH/10;
          c = new Color(Random.Range(0.8f, 1), Random.Range(0.7f, 1), Random.Range(0, 0.1f), 1.0f);
        }
        map.tiles[x,y].position = new Vector3(x, y, depth);
        map.tiles[x,y].setColor(c, width, height);
      }
    }
  }

  public void BuildTexture() {
    Texture2D texture = new Texture2D(map.width * tileSize, map.height * tileSize);

    for(int x=0; x < map.width; x++) {
      for(int y=0; y < map.height; y++) {
        texture.SetPixels(x * tileSize, y * tileSize, tileSize, tileSize, map.GetTile(x,y).color);
      }
    }

    texture.filterMode = FilterMode.Point;
    texture.wrapMode = TextureWrapMode.Clamp;
    texture.Apply();

    MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
    mesh_renderer.sharedMaterials[0].mainTexture = texture;
  }

  // roughly following the TileMap tutorial of Quill18 http://quill18.com/unity_tutorials/
  public void BuildMesh() {
    if (map == null) {
      BuildMap();
    }

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
        vertices[i] = new Vector3( x * 1.0f, 0, - y * 1.0f);
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

  void Update () {
  }
}
