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
		int x1 = x+1;
		while(IsMatching(x, y, x1, y)) {
			rMatch++;
			x1++;
		}

		x1 = x-1;
		while(IsMatching(x, y, x1, y)) {
			lMatch--;
			x1--;
		}

		if(rMatch - lMatch >= 2) {
			for(x1 = lMatch; x1 <= rMatch; x1++)
				ShiftDown(x1, y);
			
			Debug.Log("horizontal match");
			return rMatch - lMatch;
		}

		lMatch = y; rMatch = y;

        //Check vertical-down
      	int y1 = y + 1;
        while (IsMatching(x, y, x, y1))
        {
            rMatch++;
            y1++;
        }

        //Check vertical-up
        y1 = y - 1;
        while (IsMatching(x, y, x, y1))
        {
            lMatch--;
            y1--;
        }

        if (rMatch - lMatch >= 2)
        {
            for (y1 = lMatch; y1 <= rMatch; y1++) {
				ShiftDown(x, y1);
				Debug.Log("Match " + x + " " + y1);
			}
			
			Debug.Log("vertical match");
            return rMatch - lMatch;
        }


		//No matches found
		return -1;
	}

	///Shifts tiles down to fill the given spaces.
	///Input: Coordinate to shift tiles down into. 
	public void ShiftDown(int x, int y) {
		//Loop through from y=y to y=0. (Go from higher y value first.)

			//Inside loop:
			//Set the tile at (x, y+1) to the type at (x, y).
			//Remember that (0,0) is the top left corner.
			//Update the tile.

		//Generate a random tile at (x, 0).
	}

	///Returns true if the two coordinates match type.
	///Precondition: Tiles at (x1,y1) and (x2,y2) exist.
	private bool IsMatching(int x1, int y1, int x2, int y2) {
		return (GetTile(x1, y1).GetTypeID() == GetTile(x2, y2).GetTypeID());
	}

}
