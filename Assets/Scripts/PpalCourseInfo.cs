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
	private QuizesDAO db2;
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
		db2 = new QuizesDAO ();
		playerPrefsDAO = new PlayerPrefsDAO ();
		update ();
	}

	private void update ()
	{
		name.text = playerPrefsDAO.getName ();
		email.text = playerPrefsDAO.getEmail ();
		//karma.text = "Karma: No impplemented yet";
		//rank.text = "No implemented yet";
		int karmaInt = db2.getResolvedCount();
		karma.text = "Karma: " + karmaInt.ToString();
		assignCategory (karmaInt);
		//rank.text = "Primiparo";
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

	void assignCategory(int karma){
		string category = "";
		if (karma < 10)
			category = "primiparo";
		else if (karma >= 10 && karma < 20)
			category = "Estudiante curioso";
		else if (karma >= 20 && karma < 30)
			category = "Estudiante experimentado";
		else if (karma >= 30 && karma < 50)
			category = "Estudiante conocedor";
		else if (karma >= 50)
			category = "Univalluno";
		rank.text = category;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
