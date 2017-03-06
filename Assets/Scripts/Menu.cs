using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	private Text name;
	private Text email;
	private PlayerPrefsDAO playerPrefsDAO;

	void Start () {
		name = GameObject.Find ("NameUI").GetComponent<Text> ();
		email = GameObject.Find ("EmailUI").GetComponent<Text> ();
		playerPrefsDAO = new PlayerPrefsDAO ();

		loadPrefs ();
	}

	private void loadPrefs(){
		name.text = playerPrefsDAO.getName ();
		email.text = playerPrefsDAO.getEmail ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
