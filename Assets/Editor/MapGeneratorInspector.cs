
using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorInspector : Editor {
	
	public override void OnInspectorGUI() {
		//base.OnInspectorGUI();
		DrawDefaultInspector();
		
		if(GUILayout.Button("Regenerate")) {
			MapGenerator map = (MapGenerator)target;
			map.BuildMesh();
		}
	}
}
