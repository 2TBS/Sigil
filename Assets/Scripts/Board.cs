﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Attach to Board
public class Board : MonoBehaviour {

	public GameObject board;
	public Tile[] tiles;

	public int swapID;
	public bool swapping;

	// Use this for initialization
	void Start () {

		tiles = board.GetComponentsInChildren<Tile>();

		int x = 0;

		//Assign tile type and coordinates
		foreach(Tile tile in tiles) {
			tile.SetType(Random.Range(0,4));
			tile.UpdateItem();
			tile.SetCoordinates(x % 8, x/8);

			x++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Tile GetTile(int id) {
		try {
			return GameObject.Find("Tile (" +id + ")").GetComponent<Tile>();
		} catch {
			Debug.Log("Could Not Find Tile " + id);
			return GameObject.Find("Tile (0)").GetComponent<Tile>();
		}
	}

	public Tile GetTile() {
		return GetTile(swapID);
	}
}
