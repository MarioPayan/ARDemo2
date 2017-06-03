using System.Collections;
using System.Collections.Generic;
using Vuforia;
using UnityEngine;

public class CourseTracker : MonoBehaviour, ITrackableEventHandler {

	private TrackableBehaviour mTrackableBehaviour;

	[SerializeField]
	private string courseCode;
	private GameObject cursoInfo;
	private bool previousDetected;

	void Start () {
		cursoInfo = CourseInfo.cursoInfo;

		previousDetected = false;

		mTrackableBehaviour = GetComponent<TrackableBehaviour> ();
		if (mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler (this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTrackableStateChanged (
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED) {
			cursoInfo.SetActive (true);
			cursoInfo.SendMessage ("changeInfo", courseCode);
			previousDetected = true;

		} else {
			if (previousDetected) {
				cursoInfo.SendMessage ("saveCourseChanges");
				cursoInfo.SetActive (false);
				previousDetected = false;
			}

		}
	}
}
