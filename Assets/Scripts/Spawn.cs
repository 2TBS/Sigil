using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Attach to Board
public class Spawn : MonoBehaviour {

	public GameObject board;
	public Button[] tiles;

	// Use this for initialization
	void Start () {

		tiles = board.GetComponentsInChildren<Button>();
		foreach(Button tile in tiles) {
			tile.SetType(Random.Range(0,4));
			tile.UpdateItem();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
