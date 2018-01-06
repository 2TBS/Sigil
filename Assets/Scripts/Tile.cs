using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

	///0 through 4. Type of tile.
	private int type;
	private Board board;

	public int x, y;
	public GameObject[] tileObjects;

	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(ClickAction);
		board = (Board)FindObjectOfType(typeof(Board));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateItem() {
		try {
			Destroy(GetComponentsInChildren<GameObject>()[1]);
		} catch {} finally {
			GameObject newItem = Instantiate(tileObjects[type], gameObject.transform.position, Quaternion.identity);
            newItem.transform.SetParent(transform);
		}
		
	}
	
	public void SetType(int type) {
		this.type = type;
	}

	public int GetTypeID() {
		return type;
	}

	public void SetCoordinates(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public void ClickAction() {

		
		if(!board.swapping) {
			board.swapping = true;
			board.swapID = 8*y + x;
		}
		else {
			if(Mathf.Abs(x - board.GetTile().x) <= 1 && Mathf.Abs(y- board.GetTile().y) <= 1) {
				int temp = type;
				type = board.GetTile().GetTypeID();
				board.GetTile().SetType(temp);
				UpdateItem();
				board.GetTile().UpdateItem();
			}
			
			board.swapping = false;
		}

		Debug.Log(board.swapping);
	}
}
