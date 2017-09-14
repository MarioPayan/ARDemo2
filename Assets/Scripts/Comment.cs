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
}
