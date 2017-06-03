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
	private WWWForm form;
	private Dictionary<string,string> headers;
	private GameObject commentsInfo;
	private GameObject login;
	private PlayerPrefsDAO playerPrefsDAO;

	void Start ()
	{
		commentsInfo = CommentsInfo.commentsInfo;
		login = GameObject.Find ("LoginCanvas");
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
		WWW www = new WWW (usersUrl, null, headers);
		StartCoroutine (WaitForRequest (www, "userGET"));
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
		Debug.Log ("posteando usuario");
		initialize ();
		form.AddField ("username", username);
		form.AddField ("password", password);
		form.AddField ("email", email);
		byte[] rawData = form.data;
		WWW www = new WWW (usersUrl, rawData, headers);
		StartCoroutine (WaitForRequest (www, "userPOST"));
	}

	public void checkUserAvailable(string username){
		form = new WWWForm ();
		headers = form.headers;
		string credentials = "Basic " + System.Convert.ToBase64String (System.Text.Encoding.ASCII.GetBytes ("newuser:createnewuser"));
		headers.Add ("Authorization", credentials);
		string consultUser = usersUrl + username;
		WWW www = new WWW (consultUser, null, headers);
		StartCoroutine (WaitForRequest (www, "checkUser"));
	}

	IEnumerator WaitForRequest (WWW www, string type)
	{
		yield return www;
		if (www.error == null) {
			switch (type) 
			{
			case "commentGET":
				commentsInfo.SendMessage ("loadComments", www.data);
				break;
			case "upvote":
				Debug.Log ("upvote: " + www.data);
				break;
			case "downvote":
				Debug.Log ("downvote: " + www.data);
				break;
			case "clearvote":
				Debug.Log ("clearvote: " + www.data);
				break;
			case "commentPOST":
				Debug.Log("commentPOST: " + www.data);
				break;
			case "userPOST":
				Debug.Log ("userPOST: " + www.data);
				break;
			case "checkUser":
				login.SendMessage ("checkUser", www.data);
				Debug.Log ("checkUser: " + www.data);
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
