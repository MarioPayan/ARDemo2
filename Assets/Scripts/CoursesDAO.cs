using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoursesDAO : MonoBehaviour {

	SqliteDatabase sqlDB = new SqliteDatabase("DB.db");

	public CoursesDAO () {
		
	}

	public string getCode (string id)
	{
		string query = "SELECT code FROM course WHERE id='"+id+"';";
		DataTable result = sqlDB.ExecuteQuery(query);
		return (string) result.Rows[0]["code"];
	}

	public string getTitle (string course)
	{
		string query = "SELECT title FROM course WHERE code='"+course+"';";
		DataTable result = sqlDB.ExecuteQuery(query);
		return (string) result.Rows[0]["title"];
	}

	public int getCredits (string course)
	{
		string query = "SELECT credits FROM course WHERE code='"+course+"';";
		DataTable result = sqlDB.ExecuteQuery(query);
		return (int) result.Rows[0]["credits"];
	}

	public string getDescription (string course)
	{
		string query = "SELECT description FROM course WHERE code='"+course+"';";
		DataTable result = sqlDB.ExecuteQuery(query);
		return (string) result.Rows[0]["description"];
	}

	public string getLink(string course){
		string query = "SELECT link FROM course WHERE code='"+course+"';";
		DataTable result = sqlDB.ExecuteQuery(query);
		return (string) result.Rows[0]["link"];
	}

	public bool getCoursed (string course)
	{
		string query = "SELECT coursed FROM course WHERE code='"+course+"';";
		DataTable result = sqlDB.ExecuteQuery(query);
		return ((int) result.Rows[0]["coursed"] == 1) ? true : false;
	}

	public float getNote (string course)
	{
		string query = "SELECT note FROM course WHERE code='"+course+"';";
		DataTable result = sqlDB.ExecuteQuery(query);
		return float.Parse(result.Rows [0] ["note"] + " ");
	}

	public void setCoursed (string course, bool coursed)
	{
		int coursedInt = (coursed) ? 1 : 0;
		string query = "UPDATE Course SET coursed = "+coursedInt+" WHERE code = '"+course+"';";
		sqlDB.ExecuteNonQuery(query);
	}

	public void setNote (string course, float note)
	{
		string query = "UPDATE Course SET note = "+note.ToString()+" WHERE code = '"+course+"';";
		sqlDB.ExecuteNonQuery(query);
	}

	public void saveModifiedCourse (string course, bool coursed, float note)
	{
		setCoursed (course, coursed);
		note = (coursed) ? note : 0;
		setNote (course, note);
	}

	public float getAverage ()
	{
		string query = "SELECT SUM(note*credits)/SUM(credits) AS average FROM (SELECT note,credits FROM Course WHERE coursed = 1)";
		DataTable result = sqlDB.ExecuteQuery(query);
		try{
			return float.Parse(result.Rows [0] ["average"] + " ");
		}catch(Exception){
			return 0;
		}
	}

	public int getApprovedCredits ()
	{
		string query = "SELECT SUM(credits) AS approved_credits FROM (SELECT credits FROM Course WHERE coursed = 1 AND note>=3)";
		DataTable result = sqlDB.ExecuteQuery(query);
		try{
			return (int) result.Rows[0]["approved_credits"];
		}catch(Exception){
			return 0;
		}
	}

	public int getCoursedCredits ()
	{
		string query = "SELECT SUM(credits) AS coursed_credits FROM (SELECT credits FROM Course WHERE coursed = 1)";
		DataTable result = sqlDB.ExecuteQuery(query);
		try{
			return (int) result.Rows[0]["coursed_credits"];
		}catch(Exception){
			return 0;
		}
	}

	public int getTotalCredits ()
	{
		string query = "SELECT SUM(credits) AS total_credits FROM Course";
		DataTable result = sqlDB.ExecuteQuery(query);
		try{
			return (int) result.Rows[0]["total_credits"];
		}catch(Exception e){
			return 0;
		}
	}

	public int getApprovedCourses ()
	{
		string query = "SELECT COUNT(*) AS approved_courses FROM (SELECT credits FROM Course WHERE coursed = 1 AND note>=3)";
		DataTable result = sqlDB.ExecuteQuery(query);
		try{
			return (int) result.Rows[0]["approved_courses"];
		}catch(Exception){
			return 0;
		}
	}

	public int getCoursedCourses ()
	{
		string query = "SELECT COUNT(*) AS coursed_courses FROM (SELECT credits FROM Course WHERE coursed = 1)";
		DataTable result = sqlDB.ExecuteQuery(query);
		try{
			return (int) result.Rows[0]["coursed_courses"];
		}catch(Exception){
			return 0;
		}
	}

	public int getTotalCourses ()
	{
		string query = "SELECT COUNT(*) AS total_courses FROM Course";
		DataTable result = sqlDB.ExecuteQuery(query);
		try{
			return (int) result.Rows[0]["total_courses"];
		}catch(Exception){
			return 0;
		}
	}
}
