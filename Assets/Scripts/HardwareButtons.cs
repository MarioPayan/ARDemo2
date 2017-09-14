using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardwareButtons : MonoBehaviour {
	private SceneLoader sl;

	// Use this for initialization
	void Start () {
		sl = new SceneLoader ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape))
			sl.LoadNewSceneGO ("Menu");
	}
}
