using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System;

public class CommentsInfo : MonoBehaviour
{
	public static GameObject commentsInfo;
	private GameObject commentPanel;
	private ApiRestDAO apiRestDAO;
	private CommentList commentList;
	private string marker;

	private Text[] commentTexts;
	private Button[] upVoteButtons;
	private Button[] downVoteButtons;
	private Text[] karmaTexts;

	private Button createCommentButton;
	private Button prevButton;
	private Button nextButton;

	private Text postCommentText;
	private Button postCommentButton;

	void Start ()
	{
		apiRestDAO = gameObject.AddComponent (typeof(ApiRestDAO)) as ApiRestDAO;
		commentTexts = new Text [10];
		karmaTexts = new Text[10];
		upVoteButtons = new Button[10];
		downVoteButtons = new Button[10];

		for (int i = 0; i < commentTexts.Length; i++) {
			commentTexts [i] = GameObject.Find ("CommentTextUI" + i.ToString ()).GetComponent<Text> ();
			karmaTexts [i] = GameObject.Find ("KarmaUI" + i.ToString ()).GetComponent<Text> ();
			upVoteButtons [i] = GameObject.Find ("UpVoteButtonUI" + i.ToString ()).GetComponent<Button> ();
			downVoteButtons [i] = GameObject.Find ("DownVoteButtonUI" + i.ToString ()).GetComponent<Button> ();
		}

		createCommentButton = GameObject.Find ("CreateCommentButton").GetComponent<Button> ();
		prevButton = GameObject.Find ("PrevButtonUI").GetComponent<Button> ();
		nextButton = GameObject.Find ("NextButtonUI").GetComponent<Button> ();

		commentPanel.SetActive (false);
		commentsInfo.SetActive (false);
	}

	void Awake ()
	{
		commentsInfo = GameObject.Find ("CommentsCanvas");
		commentPanel = GameObject.Find ("CommentPanelUI");
	}

	private void setArrow (Button arrow, bool canVote, string direction)
	{
		arrow.interactable = canVote;
		if (direction == "up") {
			if (canVote) {
				arrow.image.sprite = Resources.Load <Sprite> ("votes/up-deactivate");
			} else {
				arrow.image.sprite = Resources.Load <Sprite> ("votes/up-activate");
			}
		} else {
			if (canVote) {
				arrow.image.sprite = Resources.Load <Sprite> ("votes/down-deactivate");
			} else {
				arrow.image.sprite = Resources.Load <Sprite> ("votes/down-activate");

			}
		}
	}

	private void upVoteUI (int id, int state)
	{
		if (state == -1) {
			apiRestDAO.clearVote (id.ToString ());
		} else {
			apiRestDAO.upVote (id.ToString ());
		}

	}

	private void downVoteUI (int id, int state)
	{
		if (state == 1) {
			apiRestDAO.clearVote (id.ToString ());
		} else {
			apiRestDAO.downVote (id.ToString ());
		}
	}

	private void changeKarma (Text karmaObject, int num)
	{
		karmaObject.text = (Int32.Parse (karmaObject.text) + num).ToString ();
	}

	private void setArrows (int voted, Button upVote, Button downVote)
	{
		if (voted == 1) {
			setArrow (upVote, false, "up");
			setArrow (downVote, true, "down");
		} else if (voted == -1) {
			setArrow (upVote, true, "up");
			setArrow (downVote, false, "down");
		} else if (voted == 0) {
			setArrow (upVote, true, "up");
			setArrow (downVote, true, "down");
		}
	}

	private void loadComments (string data)
	{
		resetCommentsUI ();
		commentList = JsonUtility.FromJson<CommentList> (data);
		for (int i = 0; i < commentList.results.Count; i++) {
			commentTexts [i].text = commentList.results [i].body;
			karmaTexts [i].text = commentList.results [i].karma.ToString ();
			setArrows (commentList.results [i].voted, upVoteButtons [i], downVoteButtons [i]);
			int tmpi = i;
			upVoteButtons [i].onClick.AddListener (() => {
				upVoteUI (commentList.results [tmpi].id, commentList.results [tmpi].voted);
				changeKarma (karmaTexts [tmpi], 1);
				commentList.results [tmpi].voted += 1;
				setArrows (commentList.results [tmpi].voted, upVoteButtons [tmpi], downVoteButtons [tmpi]);
			}
			);
			downVoteButtons [i].onClick.AddListener (() => {
				downVoteUI (commentList.results [tmpi].id, commentList.results [tmpi].voted);
				changeKarma (karmaTexts [tmpi], -1);
				commentList.results [tmpi].voted += -1;
				setArrows (commentList.results [tmpi].voted, upVoteButtons [tmpi], downVoteButtons [tmpi]);
			}
			);
		}
		for (int i = commentList.results.Count; i < 10; i++) {
			commentTexts [i].text = "";
		}
		prevButton.onClick.AddListener (() => {
			prevUI (commentList.previous);
		});
		nextButton.onClick.AddListener (() => {
			nextUI (commentList.next);
		});
		createCommentButton.onClick.AddListener (() => {
			postCommentUI ();
		});
	}

	private void resetCommentsUI ()
	{
		for (int i = 0; i < 10; i++) {
			commentTexts [i].text = "Cargando...";
			karmaTexts [i].text = "0";
			setArrow (upVoteButtons [i], true, "up");
			setArrow (downVoteButtons [i], true, "down");
		}
	}

	private void postCommentUI ()
	{
		commentPanel.SetActive (true);
		postCommentText = GameObject.Find ("PostCommentTextUI").GetComponent<Text> ();
		postCommentButton = GameObject.Find ("ButtonPostCommentUI").GetComponent<Button> ();

		postCommentButton.onClick.AddListener (() => {
			sendComment ();
		});
	}

	private void sendComment ()
	{
		apiRestDAO.PostComment (1, postCommentText.text, marker);
	}

	private void getCommentUI (string markerComment)
	{
		marker = markerComment;
		apiRestDAO.getComment (markerComment, "karma", 0);
	}

	private void nextUI (string consult)
	{
		if (consult != "")
			apiRestDAO.changePage (consult);
	}

	private void prevUI (string consult)
	{
		if (consult != "")
			apiRestDAO.changePage (consult);
	}

	void Update ()
	{
	}
}
