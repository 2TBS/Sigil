using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

	///0 through 4. Type of tile.
	private int type;
	public GameObject[] tileObjects;

	// Use this for initialization
	void Start () {
		
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
}
