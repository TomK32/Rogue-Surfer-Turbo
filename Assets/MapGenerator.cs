using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
 
public class MapGenerator : MonoBehaviour {
       
  // Map Generation Parameters
  public const int MAX_DEPTH = 10;
  public float tileSize = 1.0f;

  public int seed;
  public Map map;
       
  void Start() {
    BuildMap();
    BuildMesh();
  }

  public void BuildMap() {
    int width = (int)Screen.width / 8;
    int height = (int)Screen.height / 8;
    print(height);
    float depth = 0.0f;

    map = new Map(width, height);

	  for (int x = 0; x < width; x++) {
      for (int y=0; y < height; y++) {
        if (y > height * 0.2) {
          map.tiles[x,y] = new Ocean();
          depth = y * - MAX_DEPTH;
        } else {
          map.tiles[x,y] = new Beach();
          depth = x * MAX_DEPTH/10;
        }
        map.tiles[x,y].position = new Vector3(x, y, depth);
      }
    }
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
        vertices[i] = new Vector3( x * tileSize, 0, - y * tileSize);
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
    print(mesh);
  }
       
  void Update () {
  }
}
