using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizesDAO : MonoBehaviour {

	SqliteDatabase sqlDB = new SqliteDatabase("DB.db");

	public QuizesDAO () {}

	public string getQuestion (string id)
	{
		string query = "SELECT question FROM quizes WHERE id='"+id+"';";
		DataTable result = sqlDB.ExecuteQuery(query);
		return (string) result.Rows[0]["question"];
	}

	public string getAnswer1 (string id)
	{
		string query = "SELECT answer1 FROM quizes WHERE id='"+id+"';";
		DataTable result = sqlDB.ExecuteQuery(query);
		return (string) result.Rows[0]["answer1"];
	}

	public string getAnswer2 (string id)
	{
		string query = "SELECT answer2 FROM quizes WHERE id='"+id+"';";
		DataTable result = sqlDB.ExecuteQuery(query);
		return (string) result.Rows[0]["answer2"];
	}

	public string getAnswer3 (string id)
	{
		string query = "SELECT answer3 FROM quizes WHERE id='"+id+"';";
		DataTable result = sqlDB.ExecuteQuery(query);
		return (string) result.Rows[0]["answer3"];
	}

	public string getAnswer4 (string id)
	{
		string query = "SELECT answer4 FROM quizes WHERE id='"+id+"';";
		DataTable result = sqlDB.ExecuteQuery(query);
		return (string) result.Rows[0]["answer4"];
	}

	public int getAnswerNumber (string id)
	{
		string query = "SELECT answer_number FROM quizes WHERE id='"+id+"';";
		DataTable result = sqlDB.ExecuteQuery(query);
		return (int) result.Rows[0]["answer_number"];
	}

	public int getCount ()
	{
		string query = "SELECT COUNT(*) AS count FROM quizes;";
		DataTable result = sqlDB.ExecuteQuery(query);
		return (int) result.Rows[0]["count"];
	}

	public int getResolvedCount ()
	{
		string query = "SELECT COUNT(*) AS count FROM quizes WHERE resolved = 1;";
		DataTable result = sqlDB.ExecuteQuery(query);
		return (int) result.Rows[0]["count"];
	}

	public void setResolved (int id)
	{
		string query = "UPDATE quizes SET resolved = 1 WHERE id = '"+id+"';";
		sqlDB.ExecuteNonQuery(query);
	}
}
