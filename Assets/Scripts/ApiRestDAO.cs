using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApiRestDAO : MonoBehaviour
{

	private string commentsUrl = "https://django-rest-mariopayan.c9users.io/comments/";
	private string usersUrl = "https://django-rest-mariopayan.c9users.io/users/";
	private string commentsKarma = "https://django-rest-mariopayan.c9users.io/comments-marker-by-karma/";
	private string commentsDate = "https://django-rest-mariopayan.c9users.io/comments-marker-by-date/";
	private string commentVote = "https://django-rest-mariopayan.c9users.io/comments/";
	private string usersKarmaUrl = "https://django-rest-mariopayan.c9users.io/karma/";
	private WWWForm form;
	private Dictionary<string,string> headers;
	private GameObject commentsInfo;
	private GameObject login;
	private GameObject summary;
	private PlayerPrefsDAO playerPrefsDAO;

	void Start ()
	{
		commentsInfo = CommentsInfo.commentsInfo;
		login = GameObject.Find ("LoginCanvas");
		summary = GameObject.Find ("SummaryCanvas");
	}

	private void initialize ()
	{
		form = new WWWForm ();
		headers = form.headers;
		playerPrefsDAO = new PlayerPrefsDAO ();
		string user = playerPrefsDAO.getName();
		string pass = playerPrefsDAO.getPassword();
		string credentials = "Basic " + System.Convert.ToBase64String (System.Text.Encoding.ASCII.GetBytes (user + ":" + pass));
		headers.Add ("Authorization", credentials);
	}

	public void getComment (string marker, string order, int page)
	{
		initialize ();
		string orderUrl = (order == "karma") ? commentsKarma : commentsDate;
		string pageUrl = (page != 0) ? "?page=" + page.ToString() : "";
		orderUrl = orderUrl + marker + "/" + pageUrl;
		WWW www = new WWW (orderUrl, null, headers);
		StartCoroutine (WaitForRequest (www, "commentGET"));
	}

	public void changePage (string consult)
	{
		initialize ();
		Debug.Log (consult);
		WWW www = new WWW (consult, null, headers);
		StartCoroutine (WaitForRequest (www, "commentGET"));
	}

	public void upVote (string comment)
	{
		initialize ();
		string voteUrl = commentVote + comment + "/upvote/";
		WWW www = new WWW (voteUrl, null, headers);
		StartCoroutine (WaitForRequest (www, "upvote"));
	}

	public void downVote (string comment)
	{
		initialize ();
		string voteUrl = commentVote + comment + "/downvote/";
		WWW www = new WWW (voteUrl, null, headers);
		StartCoroutine (WaitForRequest (www, "downvote"));
	}

	public void clearVote (string comment)
	{
		initialize ();
		string voteUrl = commentVote + comment + "/clearvote/";
		WWW www = new WWW (voteUrl, null, headers);
		StartCoroutine (WaitForRequest (www, "clearvote"));
	}

	public void getUser (string username)
	{
		initialize ();
		WWW www = new WWW (usersUrl + "/" + username + "/", null, headers);
		StartCoroutine (WaitForRequest (www, "userGET"));
	}

	public void getUserKarma (string username)
	{
		initialize ();
		WWW www = new WWW (usersKarmaUrl, null, headers);
		StartCoroutine (WaitForRequest (www, "userKarmaGET"));
		summary.SendMessage ("karma", www.text);
	}

	public void PostComment (int id, string body, string marker)
	{
		initialize ();
		form.AddField ("owner", id);
		form.AddField ("body", body);
		form.AddField ("marker", marker);
		byte[] rawData = form.data;
		WWW www = new WWW (commentsUrl, rawData, headers);
		StartCoroutine (WaitForRequest (www, "commentPOST"));
	}

	public void PostUser (string username, string password, string email)
	{
		form = new WWWForm ();
		headers = form.headers;
		string credentials = "Basic " + System.Convert.ToBase64String (System.Text.Encoding.ASCII.GetBytes ("newuser:createnewuser"));
		headers.Add ("Authorization", credentials);
		form.AddField ("username", username);
		form.AddField ("password", password);
		form.AddField ("email", email);
		byte[] rawData = form.data;
		WWW www = new WWW (usersUrl, rawData, headers);
		StartCoroutine (WaitForRequest (www, "userPOST"));
		while (!www.isDone) {}
	}

	public void checkUserAvailable(string username){
		form = new WWWForm ();
		headers = form.headers;
		string credentials = "Basic " + System.Convert.ToBase64String (System.Text.Encoding.ASCII.GetBytes ("newuser:createnewuser"));
		headers.Add ("Authorization", credentials);
		string consultUser = usersUrl + username;
		WWW www = new WWW (consultUser, null, headers);
		StartCoroutine (WaitForRequest (www, "checkUser"));
		login.SendMessage ("checkUser", www.text);
	}

	IEnumerator WaitForRequest (WWW www, string type)
	{
		yield return www;
		if (www.error == null || type=="checkUser") {
			switch (type) 
			{
			case "commentGET":
				commentsInfo.SendMessage ("loadComments", www.text);
				break;
			case "upvote":
				Debug.Log ("upvote: " + www.text);
				break;
			case "downvote":
				Debug.Log ("downvote: " + www.text);
				break;
			case "clearvote":
				Debug.Log ("clearvote: " + www.text);
				break;
			case "commentPOST":
				Debug.Log("commentPOST: " + www.text);
				break;
			case "userPOST":
				Debug.Log ("userPOST: " + www.text);
				break;
			case "userKarmaGET":
				Debug.Log ("userKarmaGET: " + www.text);
				break;
			case "checkUser":
				login.SendMessage ("checkUser", www.text);
				Debug.Log ("checkUser: " + www.text);
				break;
			default:
				Debug.Log ("Error case");
				break;
			}
		} else {
			Debug.Log ("WWW Error: " + www.error + " | " + www.text);
		}    
	}
}
