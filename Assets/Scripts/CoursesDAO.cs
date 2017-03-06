using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoursesDAO : MonoBehaviour {

	SqliteDatabase sqlDB = new SqliteDatabase("DB.db");

	public CoursesDAO () {
		
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

	public void setTitle (string course, string title)
	{

	}

	public void setCredits (string course, int credits)
	{

	}

	public void setDescription (string course, string description)
	{

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
		return float.Parse(result.Rows [0] ["average"] + " ");
	}

	public int getApprovedCredits ()
	{
		string query = "SELECT SUM(credits) AS approved_credits FROM (SELECT credits FROM Course WHERE coursed = 1)";
		DataTable result = sqlDB.ExecuteQuery(query);
		return (int) result.Rows[0]["approved_credits"];
	}

	public int getTotalCredits ()
	{
		string query = "SELECT SUM(credits) AS total_credits FROM Course";
		DataTable result = sqlDB.ExecuteQuery(query);
		return (int) result.Rows[0]["total_credits"];
	}
}
