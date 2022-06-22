using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PathFinder{
[CustomEditor (typeof(PathFinderCore))]
	public class PathFinderCoreGUI : Editor{
	    public override void OnInspectorGUI () {
			PathFinderCore pathCore = (PathFinderCore)target;
			pathCore.showGizmos = EditorGUILayout.Toggle("Show Gizmos", pathCore.showGizmos);
			pathCore.deltaX = EditorGUILayout.Vector2Field("Delta (Xmin, Xmax):", pathCore.deltaX);
			pathCore.deltaY = EditorGUILayout.Vector2Field("Delta (Ymin, Ymax):", pathCore.deltaY);
			EditorGUILayout.LabelField( "Hand add");
			pathCore.oneNode = EditorGUILayout.ObjectField("First node", pathCore.oneNode, typeof(Transform), true) as Transform;
			pathCore.twoNode = EditorGUILayout.ObjectField("Second node", pathCore.twoNode, typeof(Transform), true) as Transform;
			GUILayout.BeginHorizontal("box");
	       		if (GUILayout.Button("Add")){
		            pathCore.AddRelation();
	        	}
	        	if (GUILayout.Button("Remove")){
		            pathCore.RemoveRelation();
	        	}
        	GUILayout.EndHorizontal();

        	
			EditorGUILayout.LabelField( "API");
			GUILayout.BeginHorizontal("box");
			if (GUILayout.Button("Find points")){
	            pathCore.CheckList();
        	}
        	if (GUILayout.Button("Clear relation")){
	            pathCore.ClearRelation();
        	}
            if (GUILayout.Button("Scan")){
	            pathCore.Scan();
        	}
        	GUILayout.EndHorizontal();

        	if (GUILayout.Button("Near Point")){
	            pathCore.NearPoint();
        	}
		}
	}
}