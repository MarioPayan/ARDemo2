using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Comment {
	public int id;
	public int owner;
	public string body;
	public string marker;
	public int karma;
	public string date;
	public int voted;

	public Comment(){
		id     = -1;
		owner  = -1;
		body   = "";
		marker = "";
		karma  = -1;
		date   = "";
		voted  = 0;
	}

	public void print(){
		if (id != null)	Debug.Log (id);
		if (owner != null)	Debug.Log (owner);
		if (body != null)	Debug.Log (body);
		if (marker != null)	Debug.Log (marker);

	}
}
