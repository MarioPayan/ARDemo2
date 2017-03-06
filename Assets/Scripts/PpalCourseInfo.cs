using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PpalCourseInfo : MonoBehaviour {

	public static GameObject ppalCursoInfo;
	private Text average;
	private Text credits;
	private CoursesDAO db;

	void Start () {
		average = GameObject.Find ("AverageUI").GetComponent<Text> ();
		credits = GameObject.Find ("CreditsUI").GetComponent<Text> ();
		db = new CoursesDAO ();
		update ();
		ppalCursoInfo.SetActive (false);
	}

	private void update (){
		average.text = "Promedio acumulado: " + db.getAverage ().ToString();
		credits.text = "Creditos aprobados: " + db.getApprovedCredits ().ToString() + "/" + db.getTotalCredits ().ToString();
	}

	void Awake(){
		ppalCursoInfo = GameObject.Find ("CoursesPpalCanvas");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
