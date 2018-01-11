using System.Collections;
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
			int safety = 0;
            while (tile.GetTypeID() == -1 || (safety < 10 && (GetTile(x - 1).GetTypeID() == tile.GetTypeID() || GetTile(x - 8).GetTypeID() == tile.GetTypeID())))
            {
                tile.SetType(Random.Range(0, 4));
                safety++;
                Debug.Log(safety);
            }
            tile.UpdateItem();
            tile.SetCoordinates(x % 8, x / 8);

            x++;
		}
	}

	void GenerateTile(int x, int y) {
		int safety = 0;
		Tile tile = GetTile(x,y);
        while (safety < 10 && (GetTile(x - 1).GetTypeID() == tile.GetTypeID() || GetTile(x - 8).GetTypeID() == tile.GetTypeID()))
        {
            tile.SetType(Random.Range(0, 4));
            safety++;
        }
        tile.UpdateItem();
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

	public Tile GetTile(int x, int y) {
		return GetTile(x + y*8);
	}

	public Tile GetTile() {
		return GetTile(swapID);
	}

	///Returns the length of the match, or -1 if no matches were found.
	public int CheckMatch(int x, int y) {

		int lMatch = x, rMatch = x;

		//Check horizontal-right matches first
		for(int x1 = x; x1 < 8; x1++) 
			if(IsMatching(x, y, x1, y))
				rMatch++;
		
		//Check horizontal-left
		for(int x1 = x; x1 > 0; x1--)
			if(IsMatching(x, y, x1, y))
				lMatch--;

		if(rMatch - lMatch >= 2) {
			for(int x1 = lMatch; x1 < rMatch; x1++)
				GenerateTile(x1, y);
			return rMatch - lMatch;
		}

		lMatch = y; rMatch = y;

        //Check vertical-down
        for (int y1 = y; y1 < 8; y1++)
            if (IsMatching(x, y1, x, y1))
                rMatch++;

        //Check vertical-up
        for (int y1 = y; y1 > 0; y1--)
            if (IsMatching(x, y1, x, y1))
                lMatch--;

        if (rMatch - lMatch >= 2)
        {
            for (int y1 = lMatch; y1 < rMatch; y1++) 
				GenerateTile(x, y1);
			
            return rMatch - lMatch;
        }


		//No matches found
		return -1;
	}

	///Returns true if the two coordinates match type.
	///Precondition: Tiles at (x1,y1) and (x2,y2) exist.
	private bool IsMatching(int x1, int y1, int x2, int y2) {
		return (GetTile(x1, y1).GetTypeID() == GetTile(x2, y2).GetTypeID());
	}

}
