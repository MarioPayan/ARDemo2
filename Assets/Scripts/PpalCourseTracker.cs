using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PpalCourseTracker : MonoBehaviour, ITrackableEventHandler {

	private TrackableBehaviour mTrackableBehaviour;
	private GameObject ppalCursoInfo;
	private bool previousDetected;

	void Start () {
		ppalCursoInfo = PpalCourseInfo.ppalCursoInfo;

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
			ppalCursoInfo.SetActive (true);
			previousDetected = true;

		} else {
			if (previousDetected) {
				ppalCursoInfo.SetActive (false);
				previousDetected = false;
			}

		}
	}
}
