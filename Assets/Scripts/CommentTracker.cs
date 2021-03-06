using System.Collections;
using System.Collections.Generic;
using Vuforia;
using UnityEngine;

public class CommentTracker : MonoBehaviour, ITrackableEventHandler {

	private TrackableBehaviour mTrackableBehaviour;

	[SerializeField]
	private string markerComment;
	private GameObject commentsInfo;
	private bool previousDetected;

	void Start () {
		commentsInfo = CommentsInfo.commentsInfo;
		previousDetected = false;

		mTrackableBehaviour = GetComponent<TrackableBehaviour> ();
		if (mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler (this);
		}
	}
	
	void Update () {
		
	}

	public void OnTrackableStateChanged (
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED) {
			commentsInfo.SetActive (true);
			commentsInfo.SendMessage ("getCommentUI", markerComment);
			previousDetected = true;

		} else {
			if (previousDetected) {
				commentsInfo.SendMessage ("resetCommentsUI");
				commentsInfo.SetActive (false);
				previousDetected = false;
			}

		}
	}

}
