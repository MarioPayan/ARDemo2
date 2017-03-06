using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour {

	string commentsUrl   = "https://django-rest-mariopayan.c9users.io/comments/";
	string usersUrl      = "https://django-rest-mariopayan.c9users.io/users/";
	string commentsKarma = "https://django-rest-mariopayan.c9users.io/comments-marker-by-karma/";
	string commentsDate  = "https://django-rest-mariopayan.c9users.io/comments-marker-by-date/";
	WWWForm form;
	Dictionary<string,string> headers;

	void Start () {
		//commentPOST (1, "body", "marker");
		//commentGET ("marker", "karma");
		//userPOST ("nombre", "contrasena", "email@email.com");
		userGET ();
	}

	private void initialize(){
		form = new WWWForm();
		headers = form.headers;
		string credentials = "Basic " + System.Convert.ToBase64String (System.Text.Encoding.ASCII.GetBytes ("admin:administrador"));
		headers.Add ("Authorization", credentials);
	}

	private void commentGET(string marker, string order){
		initialize ();
		string orderUrl = (order == "date") ? commentsDate : (order == "karma") ? commentsKarma : "error";
		orderUrl = orderUrl + marker + "/";
		WWW www = new WWW(orderUrl, null, headers);
		StartCoroutine(WaitForRequest(www));
	}

	private void userGET(){
		initialize ();
		WWW www = new WWW(usersUrl, null, headers);
		StartCoroutine(WaitForRequest(www));
	}

	private void commentPOST(int id, string body, string marker){
		initialize ();
		form.AddField("owner" , id);
		form.AddField("body"  , body);
		form.AddField("marker", marker);
		byte[] rawData = form.data;
		WWW www = new WWW(commentsUrl, rawData, headers);
		StartCoroutine(WaitForRequest(www));
	}

	private void userPOST(string username, string password, string email){
		initialize ();
		form.AddField("username" , username);
		form.AddField("password" , password);
		form.AddField("email"    , email);
		byte[] rawData = form.data;
		WWW www = new WWW(usersUrl, rawData, headers);
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: "  + www.data);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}
}
