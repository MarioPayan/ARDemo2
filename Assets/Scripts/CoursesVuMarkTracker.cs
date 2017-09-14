using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using Vuforia;

public class CoursesVuMarkTracker : MonoBehaviour
{
	private VuMarkManager mVuMarkManager;
	private VuMarkTarget mClosestVuMark;
	private VuMarkTarget mCurrentVuMark;
	private GameObject cursoInfo;
	private bool previousDetected;

	void Start()
	{
		cursoInfo = CourseInfo.cursoInfo;
		previousDetected = false;
		mVuMarkManager = TrackerManager.Instance.GetStateManager().GetVuMarkManager();
		mVuMarkManager.RegisterVuMarkDetectedCallback(OnVuMarkDetected);
		mVuMarkManager.RegisterVuMarkLostCallback(OnVuMarkLost);
	}

	void Update()
	{
		UpdateClosestTarget();
	}

	void OnDestroy()
	{
		mVuMarkManager.UnregisterVuMarkDetectedCallback(OnVuMarkDetected);
		mVuMarkManager.UnregisterVuMarkLostCallback(OnVuMarkLost);
	}

	public void OnVuMarkDetected(VuMarkTarget target)
	{
		string strTarget = GetVuMarkString (target);
		int intTarget = Int32.Parse(strTarget);
		if (intTarget >= 1 && intTarget <= 53) {
			cursoInfo.SetActive (true);
			cursoInfo.SendMessage ("changeInfo", strTarget);
			previousDetected = true;
		}
	}

	public void OnVuMarkLost(VuMarkTarget target)
	{
		cursoInfo.SendMessage ("saveCourseChanges");
		cursoInfo.SetActive (false);
		previousDetected = false;
	}

	void UpdateClosestTarget()
	{
		Camera cam = DigitalEyewearARController.Instance.PrimaryCamera ?? Camera.main;

		float closestDistance = Mathf.Infinity;

		foreach (var bhvr in mVuMarkManager.GetActiveBehaviours())
		{
			Vector3 worldPosition = bhvr.transform.position;
			Vector3 camPosition = cam.transform.InverseTransformPoint(worldPosition);

			float distance = Vector3.Distance(Vector2.zero, camPosition);
			if (distance < closestDistance)
			{
				closestDistance = distance;
				mClosestVuMark = bhvr.VuMarkTarget;
			}
		}

		if (mClosestVuMark != null &&
			mCurrentVuMark != mClosestVuMark)
		{
			var vuMarkId = GetVuMarkString(mClosestVuMark);
			var vuMarkTitle = GetVuMarkDataType(mClosestVuMark);
			mCurrentVuMark = mClosestVuMark;
		}
	}

	private string GetVuMarkDataType(VuMarkTarget vumark)
	{
		switch (vumark.InstanceId.DataType)
		{
		case InstanceIdType.BYTES:
			return "Bytes";
		case InstanceIdType.STRING:
			return "String";
		case InstanceIdType.NUMERIC:
			return "Numeric";
		}
		return "";
	}

	private string GetVuMarkString(VuMarkTarget vumark)
	{
		switch (vumark.InstanceId.DataType)
		{
		case InstanceIdType.BYTES:
			return vumark.InstanceId.HexStringValue;
		case InstanceIdType.STRING:
			return vumark.InstanceId.StringValue;
		case InstanceIdType.NUMERIC:
			return vumark.InstanceId.NumericValue.ToString();
		}
		return "";
	}

	private void GetVuMarkImage(VuMarkTarget vumark)
	{
		var instanceImg = vumark.InstanceImage;
		if (instanceImg == null)
		{
			Debug.Log("VuMark Instance Image is null.");
		}
	}
}

