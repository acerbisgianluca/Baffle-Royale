using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeMapBehaviour : MonoBehaviour {

	private GameObject map;
	[SerializeField] private int opacity;

	void Start () {
		map = this.gameObject;
	}
	
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Player"){
			Tilemap map = gameObject.GetComponent<Tilemap>();
			//TileBase[] allTiles = map.GetTilesBlock(map.cellBounds);
			//for(int i = 0; i < allTiles.Length; i++){
				
			//}
			//Debug.Log("COLORE_PRIMA: " + map.color);
			//map.SetColor(map.origin, /*new Color(map.GetColor(pos).r, map.GetColor(pos).g, map.GetColor(pos).b, 175)*/ Color.red);
			//Debug.Log("COLORE_DOPO: " + map.color);
			//map.RefreshAllTiles();
		}
	}
}
