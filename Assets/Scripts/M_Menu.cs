using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class M_Menu : MonoBehaviour {

    int change = 0;
    int level = 0; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadScene()
    {
        level++;
        SceneManager.LoadScene(level);
       
    }
}
