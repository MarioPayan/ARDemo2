using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CommentList {
	public int count;
	public string next;
	public string previous;
	public List<Comment> results = new List<Comment>{};

	public CommentList(){
	}
}
