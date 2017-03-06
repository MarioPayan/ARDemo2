using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class CoursesDB : MonoBehaviour
{
	private string connectionString;

	public string getTitle (string course)
	{
		CoursesDAO cdao = new CoursesDAO ();
		connectionString = "URI=file:" + Application.dataPath + "/DB.db";
		string value="";
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();
			using (IDbCommand dbCommand = dbConnection.CreateCommand ()) {
				dbCommand.CommandText = "Select title From course where code='"+course+"';";

				using (IDataReader reader = dbCommand.ExecuteReader ()) {
					while (reader.Read ()) {
						value = reader.GetString (0);
					}
					dbConnection.Close ();
					reader.Close ();
					dbCommand.Dispose ();
				}
			}
		}
		return value;
	}

	public int getCredits (string course)
	{
		connectionString = "URI=file:" + Application.dataPath + "/DB.db";
		int value=0;
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();
			using (IDbCommand dbCommand = dbConnection.CreateCommand ()) {
				dbCommand.CommandText = "Select credits From course where code='"+course+"';";

				using (IDataReader reader = dbCommand.ExecuteReader ()) {
					while (reader.Read ()) {
						value = (int)reader.GetInt32 (0);
					}
					dbConnection.Close ();
					reader.Close ();
					dbCommand.Dispose ();
				}
			}
		}
		return value;
	}

	public string getDescription (string course)
	{
		connectionString = "URI=file:" + Application.dataPath + "/DB.db";
		string value="";
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();
			using (IDbCommand dbCommand = dbConnection.CreateCommand ()) {
				dbCommand.CommandText = "Select description From course where code='"+course+"';";

				using (IDataReader reader = dbCommand.ExecuteReader ()) {
					while (reader.Read ()) {
						value = reader.GetString (0);
					}
					dbConnection.Close ();
					reader.Close ();
					dbCommand.Dispose ();
				}
			}
		}
		return value;
	}

	public bool getCoursed (string course)
	{
		connectionString = "URI=file:" + Application.dataPath + "/DB.db";
		bool value=false;
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();
			using (IDbCommand dbCommand = dbConnection.CreateCommand ()) {
				dbCommand.CommandText = "Select coursed From course where code='"+course+"';";

				using (IDataReader reader = dbCommand.ExecuteReader ()) {
					while (reader.Read ()) {
						value = reader.GetBoolean (0);
					}
					dbConnection.Close ();
					reader.Close ();
					dbCommand.Dispose ();
				}
			}
		}
		return value;
	}

	public float getNote (string course)
	{
		connectionString = "URI=file:" + Application.dataPath + "/DB.db";
		float value=0;
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();
			using (IDbCommand dbCommand = dbConnection.CreateCommand ()) {
				dbCommand.CommandText = "Select note From course where code='"+course+"';";

				using (IDataReader reader = dbCommand.ExecuteReader ()) {
					while (reader.Read ()) {
						value = reader.GetFloat (0);
					}
					dbConnection.Close ();
					reader.Close ();
					dbCommand.Dispose ();
				}
			}
		}
		return value;
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
		connectionString = "URI=file:" + Application.dataPath + "/DB.db";
		int coursedInt = (coursed) ? 1 : 0;
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();
			using (IDbCommand dbCommand = dbConnection.CreateCommand ()) {
				dbCommand.CommandText = "UPDATE Course SET coursed = "+coursedInt+" WHERE code = '"+course+"';";
				dbCommand.ExecuteNonQuery ();
				dbConnection.Close ();
				dbCommand.Dispose ();
			}
		}
	}

	public void setNote (string course, float note)
	{
		connectionString = "URI=file:" + Application.dataPath + "/DB.db";
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();
			using (IDbCommand dbCommand = dbConnection.CreateCommand ()) {
				dbCommand.CommandText = "UPDATE Course SET note = "+note.ToString()+" WHERE code = '"+course+"';";
				dbCommand.ExecuteNonQuery ();
				dbConnection.Close ();
				dbCommand.Dispose ();
			}
		}
	}

	public void saveModifiedCourse (string course, bool coursed, float note)
	{
		setCoursed (course, coursed);
		note = (coursed) ? note : 0;
		setNote (course, note);
		Debug.Log (course);
		Debug.Log (coursed);
		Debug.Log (note);
	}

	public float getAverage ()
	{
		connectionString = "URI=file:" + Application.dataPath + "/DB.db";
		float value=0;
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();
			using (IDbCommand dbCommand = dbConnection.CreateCommand ()) {
				dbCommand.CommandText = "SELECT SUM(note*credits)/SUM(credits) FROM (SELECT note,credits FROM Course WHERE coursed = 1)";

				using (IDataReader reader = dbCommand.ExecuteReader ()) {
					while (reader.Read ()) {
						value = reader.GetFloat (0);
					}
					dbConnection.Close ();
					reader.Close ();
					dbCommand.Dispose ();
				}
			}
		}
		return value;
	}

	public int getApprovedCredits ()
	{
		connectionString = "URI=file:" + Application.dataPath + "/DB.db";
		int value=0;
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();
			using (IDbCommand dbCommand = dbConnection.CreateCommand ()) {
				dbCommand.CommandText = "SELECT SUM(credits) FROM (SELECT credits FROM Course WHERE coursed = 1)";

				using (IDataReader reader = dbCommand.ExecuteReader ()) {
					while (reader.Read ()) {
						value = reader.GetInt32 (0);
					}
					dbConnection.Close ();
					reader.Close ();
					dbCommand.Dispose ();
				}
			}
		}
		return value;
	}

	public int getTotalCredits ()
	{
		connectionString = "URI=file:" + Application.dataPath + "/DB.db";
		int value=0;
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();
			using (IDbCommand dbCommand = dbConnection.CreateCommand ()) {
				dbCommand.CommandText = "SELECT SUM(credits) FROM Course";

				using (IDataReader reader = dbCommand.ExecuteReader ()) {
					while (reader.Read ()) {
						value = reader.GetInt32 (0);
					}
					dbConnection.Close ();
					reader.Close ();
					dbCommand.Dispose ();
				}
			}
		}
		return value;
	}

}
