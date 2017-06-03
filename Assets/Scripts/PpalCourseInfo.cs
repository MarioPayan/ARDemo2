using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PpalCourseInfo : MonoBehaviour
{

	public static GameObject ppalCursoInfo;
	private Text name;
	private Text email;
	private Text karma;
	private Text rank;
	private Text totalCredits;
	private Text coursedCredits;
	private Text approvedCredits;
	private Text remainCredits;
	private Text percentCompletedCredits;
	private Text totalCourses;
	private Text coursedCourses;
	private Text approvedCourses;
	private Text remainCourses;
	private Text percentCompletedCourses;
	private CoursesDAO db;
	private PlayerPrefsDAO playerPrefsDAO;

	void Start ()
	{
		name = GameObject.Find ("NameUI").GetComponent<Text> ();
		email = GameObject.Find ("EmailUI").GetComponent<Text> ();
		karma = GameObject.Find ("KarmaUI").GetComponent<Text> ();
		rank = GameObject.Find ("RankUI").GetComponent<Text> ();

		totalCredits = GameObject.Find ("TotalCreditsUI").GetComponent<Text> ();
		coursedCredits = GameObject.Find ("CoursedCreditsUI").GetComponent<Text> ();
		approvedCredits = GameObject.Find ("ApprovedCreditsUI").GetComponent<Text> ();
		remainCredits = GameObject.Find ("RemainCreditsUI").GetComponent<Text> ();
		percentCompletedCredits = GameObject.Find ("PercentCompletedCreditsUI").GetComponent<Text> ();
		totalCourses = GameObject.Find ("TotalCoursesUI").GetComponent<Text> ();
		coursedCourses = GameObject.Find ("CoursedCoursesUI").GetComponent<Text> ();
		approvedCourses = GameObject.Find ("ApprovedCoursesUI").GetComponent<Text> ();
		remainCourses = GameObject.Find ("RemainCoursesUI").GetComponent<Text> ();
		percentCompletedCourses = GameObject.Find ("PercentCompletedCoursesUI").GetComponent<Text> ();
		db = new CoursesDAO ();
		playerPrefsDAO = new PlayerPrefsDAO ();
		update ();
	}

	private void update ()
	{
		name.text = playerPrefsDAO.getName ();
		email.text = playerPrefsDAO.getEmail ();
		karma.text = "Karma: No impplemented yet";
		rank.text = "No implemented yet";
		totalCredits.text = "Total de créditos: " + db.getTotalCredits ().ToString ();
		coursedCredits.text = "Créditos cursados: " + db.getCoursedCredits ().ToString();
		approvedCredits.text = "Creditos aprobados: " + db.getApprovedCredits ().ToString ();
		remainCredits.text = "Créditos restantes: " + (db.getTotalCredits () - db.getApprovedCredits ()).ToString();
		double percentCompletedCreditsVal = Math.Round(((double)db.getApprovedCredits () / db.getTotalCredits ()) * 100,2);
		percentCompletedCredits.text = "Porcentaje de créditos aprobados: " + percentCompletedCreditsVal.ToString() + "%";
		totalCourses.text = "Total de cursos: " + db.getTotalCourses ().ToString ();
		coursedCourses.text = "Cursos vistos: " + db.getCoursedCourses ().ToString();
		approvedCourses.text = "Cursos aprobados: " + db.getApprovedCourses ().ToString ();
		remainCourses.text = "Cursos restantes: " + (db.getTotalCourses () - db.getApprovedCourses ()).ToString();
		double percentCompletedCoursesVal = Math.Round(((double)db.getApprovedCourses () / db.getTotalCourses ()) * 100,2);
		percentCompletedCourses.text = "Porcentaje de cursos aprobados: " + percentCompletedCoursesVal.ToString() + "%";
	}

	void Awake ()
	{
		ppalCursoInfo = GameObject.Find ("CoursesPpalCanvas");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
