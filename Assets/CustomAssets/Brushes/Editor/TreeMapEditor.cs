using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;


[CustomEditor(typeof(TreeMapBrush))]
public class TreeMapEditor : GridBrushEditorBase {

	public override void OnPaintInspectorGUI()
	{

        //if (BrushEditorUtility.SceneIsPrepared())
        //    GUILayout.Label("Use this custom Brush to paint TreeMap on the map!");

        //else
        //    BrushEditorUtility.UnpreparedSceneInspector();
        
    }
}
