using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User{
	public int id;
	public string username;
	public string email;

	public User(){
		id = 0;
		username = "";
		email = "";
	}
}
