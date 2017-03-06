using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsDAO : MonoBehaviour {

	void Start ()
	{

	}

	void Update ()
	{

	}

	private bool exist(string key){
		return PlayerPrefs.HasKey (key);
	}

	public void resetPrefs(){
		PlayerPrefs.DeleteAll ();
	}

	public void setName(string name){
		if (!exist (name)) {
			PlayerPrefs.SetString ("name", name);
		} else {
			Debug.Log("The key already exist");
		}
	}

	public void setEmail(string email){
		if (!exist (email)) {
			PlayerPrefs.SetString ("email", email);
		} else {
			Debug.Log("The key already exist");
		}
	}

	public string getName(){
		if (exist ("name")) {
			return PlayerPrefs.GetString ("name");
		} else {
			Debug.Log("The key does not exist");
		}
		return "";
	}

	public string getEmail(){
		if (exist ("email")) {
			return PlayerPrefs.GetString ("email");
		} else {
			Debug.Log("The key does not exist");
		}
		return "";
	}
}
