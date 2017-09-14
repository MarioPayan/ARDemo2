using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using Vuforia;

public class InformativeVuMarkerTracker : MonoBehaviour {

	private VuMarkManager mVuMarkManager;
	private VuMarkTarget mClosestVuMark;
	private VuMarkTarget mCurrentVuMark;
	private Text textTittle;
	private Text textDescription;
	private GameObject buttonCanvas;
	private GameObject rawImage;
	private Button masInfo;
	private InformativeDAO bd;

	void Start()
	{
		mVuMarkManager = TrackerManager.Instance.GetStateManager().GetVuMarkManager();
		mVuMarkManager.RegisterVuMarkDetectedCallback(OnVuMarkDetected);
		mVuMarkManager.RegisterVuMarkLostCallback(OnVuMarkLost);
		textTittle = GameObject.Find ("TittleUI").GetComponent<Text> ();
		textDescription = GameObject.Find ("DescriptionUI").GetComponent<Text> ();
		buttonCanvas = GameObject.Find ("ButtonCanvas");
		rawImage = GameObject.Find ("RawImage");
		masInfo = GameObject.Find ("LinkUI").GetComponent<Button> ();
		bd = new InformativeDAO ();
		rawImage.SetActive (false);
		buttonCanvas.SetActive (false);
		textTittle.text = "";
		textDescription.text = "";
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
		if (intTarget >= 54 && intTarget <= 85) {
			rawImage.SetActive (true);
			buttonCanvas.SetActive (true);
			textTittle.text = bd.getTittle (strTarget);
			textDescription.text = bd.getDescription (strTarget);
			masInfo.onClick.AddListener (() => {
				Application.OpenURL (bd.getLink (strTarget));
			});
		}
	}

	public void OnVuMarkLost(VuMarkTarget target)
	{
		buttonCanvas.SetActive (false);
		rawImage.SetActive (false);
		textTittle.text = "";
		textDescription.text = "";
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

