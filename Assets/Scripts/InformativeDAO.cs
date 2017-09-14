using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformativeDAO : MonoBehaviour {


	SqliteDatabase sqlDB = new SqliteDatabase("DB.db");

	public InformativeDAO () {}

	public string getTittle (string id)
	{
		string query = "SELECT tittle FROM informative WHERE id='"+id+"';";
		DataTable result = sqlDB.ExecuteQuery(query);
		Debug.Log (result);
		return (string) result.Rows[0]["tittle"];
	}

	public string getDescription (string id)
	{
		string query = "SELECT description FROM informative WHERE id='"+id+"';";
		DataTable result = sqlDB.ExecuteQuery(query);
		return (string) result.Rows[0]["description"];
	}

	public string getLink(string id){
		string query = "SELECT link FROM informative WHERE id='"+id+"';";
		DataTable result = sqlDB.ExecuteQuery(query);
		return (string) result.Rows[0]["link"];
	}
}
