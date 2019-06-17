using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeMapTileBase : TileBase {

	[SerializeField] private int prova;

	#if UNITY_EDITOR
		[MenuItem("Assets/Create/CustomAssets/TreeMap", false, 1)]
			private static void CreateTreeMapTile()
		{
			Sprite[] myTextures = InitiateSlots(); 

			if (myTextures != null)
			{ 
				Debug.Log("Loaded mySPrites");
				Debug.Log(myTextures.GetType() + "Length: " + myTextures.Length );
				Debug.Log(myTextures[0].name);
				}
			else
			{
				Debug.Log("Texture not loaded");
			}
			
			for(int i = 0; i < myTextures.Length; i++){
				string fileName = "TreeMapTiles_" + i;
				TreeMapTileBase treeMap = new TreeMapTileBase();
				treeMap.name = fileName + ".asset";
				treeMap.LoadTextures(myTextures[i]);
				AssetDatabase.CreateAsset(treeMap, "Assets/CustomAssets/TreeMapTiles/" + treeMap.name + "");
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}
			

			//string fileName = "TreeMapTiles";
			//TreeMapTileBase treeMap = new TreeMapTileBase();
			//treeMap.name = fileName + ".asset";
			//treeMap.LoadTextures(myTextures);
			//AssetDatabase.CreateAsset(treeMap, "Assets/CustomAssets/TreeMapTiles/" + treeMap.name + "");
			//AssetDatabase.SaveAssets();
			//AssetDatabase.Refresh();
		}

		public static Sprite[] InitiateSlots()
		{
			Sprite[] myTextures = Resources.LoadAll<Sprite>("tilemap6");
			return myTextures;
		}
	#endif

	Sprite[] sprites;
	Sprite sprite;

	public override void RefreshTile(Vector3Int position, ITilemap tilemap){
		Debug.Log("TESTA DI MINCHIA");
	}

	private void LoadTextures(Sprite actualSprite){
		Debug.Log("ASSEGNO");
		sprite = actualSprite;
		Debug.Log("ASSEGNATO" + actualSprite.name);
	}

	private void LoadTextures(Sprite[] actualSprites){
		Debug.Log("ASSEGNO");
		sprites = new Sprite[actualSprites.Length];
		for(int i = 0; i < sprites.Length; i++){
			sprites[i] = actualSprites[i];
		}
	}

}
