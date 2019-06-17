using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

[CustomGridBrush(false, true, false, "TreeMapBrush")]
public class TreeMapBrush : GridBrushBase {

	#if UNITY_EDITOR
    	[MenuItem("Assets/Create/CustomAssets/TreeMapBrush", false, 0)]
        private static void CreateTreeMapBrush(){
			string fileName = "TreeMapBrush";
			TreeMapBrush treeMapBrush = new TreeMapBrush();
			treeMapBrush.name = fileName + ".asset";
			AssetDatabase.CreateAsset(treeMapBrush, "Assets/CustomAssets/Brushes/" + treeMapBrush.name + "");
    	}
	#endif 

	public const string k_TreeMapLayerName = "TreeMap";
	public TileBase m_TreeMap;

    public override void Paint(GridLayout grid, GameObject layer, Vector3Int position){
		//GridInformation info = BrushUtility.GetRootGridInformation(true);
        Tilemap acids = GetAcid();
		if (acids != null)
    		PaintInternal(position, acids);
    }

    private void PaintInternal(Vector3Int position, Tilemap acid){
        acid.SetTile(position, m_TreeMap);
    }

	public static Tilemap GetAcid(){
		GameObject go = GameObject.Find(k_TreeMapLayerName);
		return go != null ? go.GetComponent<Tilemap>() : null;
    }

}
