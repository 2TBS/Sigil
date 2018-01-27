using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

	///0 through 4. Type of tile.
	private int type = -1;

	///Board object that this Tile is a child of.
	private Board board;

	//Coordinates
	public int x, y;

	///List of tile prefabs, assign during runtime.
	public GameObject[] tileObjects;

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(ClickAction);
		board = (Board)FindObjectOfType(typeof(Board));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	///Destroy tile, then regenerate a new tile. Good for using after changing the type.
	public void UpdateItem() {

		try {
			Destroy(GetComponentsInChildren<GameObject>()[1]);
		} catch {} finally {
			GameObject newItem = Instantiate(tileObjects[type], gameObject.transform.position, Quaternion.identity);
            newItem.transform.SetParent(transform);
		}
		
		// try {
		// 	Debug.Log(board.CheckMatch(x, y));
		// } catch {}
	}
	
	///Sets the tile type to a different type, but does not update the tile visually.
	///Valid types: 0 to tileObjects.Length - 1
	public void SetType(int type) {
		if(type < tileObjects.Length)
			this.type = type;
		else
			Debug.Log("Invalid Tile Type");
	}


	///Returns type ID
	public int GetTypeID() {
		return type;
	}

	///Set coordinates to a new value. Use ONLY for initial coordinate assignment!
	///If you wish to swap two tiles, change their types instead.
	public void SetCoordinates(int x, int y) {
		this.x = x;
		this.y = y;
	}

	///Button Action (when tile is clicked)
	public void ClickAction() {

		//Assign swap ID and change swap status to true, if not currently swapping
		if(!board.swapping) {
			board.swapping = true;
			board.swapID = 8*y + x;
		}
		else { //2nd click after swap initiated

			//Check to make sure that the two selected tiles are adjacent from each other, but not diagonal
			if(Mathf.Abs(x - board.GetTile().x) == 1 ^ Mathf.Abs(y- board.GetTile().y) == 1) {

				//Swap types
				int temp = type;
				type = board.GetTile().GetTypeID();
				board.GetTile().SetType(temp);
				UpdateItem();
				board.GetTile().UpdateItem();

				//Check for matches after swap
				Debug.Log(board.CheckMatch(x,y));
				board.CheckMatch(board.GetTile().x, board.GetTile().y);
			}

			board.swapping = false;
		}

		Debug.Log(board.swapping);
	}
}
