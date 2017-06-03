using UnityEngine;
using System.Collections;
using Vuforia;
using UnityEngine.UI;

public class CourseInfo : MonoBehaviour
{
	public static GameObject cursoInfo;
	private string courseCode;
	private Text cursoCodigo;
	private Text cursoTitulo;
	private Text cursoCreditos;
	private Text cursoDescripcion;
	private string cursoEnlace;
	private Button cursoMasInfo;
	private InputField cursoNota;
	private Toggle cursoAprobado;
	private CoursesDAO db;

	void Start ()
	{
		db = new CoursesDAO ();
		cursoCodigo = GameObject.Find ("CourseCodeUI").GetComponent<Text> ();
		cursoNota = GameObject.Find ("CourseNoteUI").GetComponent<InputField> ();
		cursoAprobado = GameObject.Find ("CourseApprovedUI").GetComponent<Toggle> ();
		cursoTitulo = GameObject.Find ("CourseTitleUI").GetComponent<Text> ();
		cursoCreditos = GameObject.Find ("CourseCreditsUI").GetComponent<Text> ();
		cursoDescripcion = GameObject.Find ("CourseDescriptionUI").GetComponent<Text> ();
		cursoEnlace = "null";
		cursoMasInfo = GameObject.Find ("CourseLinkUI").GetComponent<Button> ();

		cursoInfo.SetActive (false);
	}

	void Awake(){
		cursoInfo = GameObject.Find ("CourseInfoCanvas");
	}

	private void changeInfo(string courseID){
		this.courseCode = db.getCode (courseID);
		cursoCodigo.text = "Código: " + courseCode;
		cursoTitulo.text = db.getTitle (courseCode);
		cursoCreditos.text = "Créditos: " + db.getCredits (courseCode).ToString ();
		cursoDescripcion.text = db.getDescription (courseCode);
		cursoEnlace = db.getLink (courseCode);
		//print (cursoEnlace);
		if (db.getCoursed (courseCode)) {
			cursoAprobado.isOn = true;
		} else {
			cursoAprobado.isOn = false;
		}
		cursoNota.text = db.getNote (courseCode).ToString ();
		cursoMasInfo.onClick.AddListener (() => {
			moreInfo ();
		});
	}

	void Update ()
	{
		if (cursoAprobado.isOn) {
			cursoNota.gameObject.SetActive (true);
		} else {
			cursoNota.gameObject.SetActive (false);
		}
	}

	private bool validateNote (string note)
	{
		bool valid = false;
		if (note != "" && note [0] != '-') {
			float noteFloat = float.Parse (note);
			if (noteFloat >= 0 && noteFloat <= 5) {
				valid = true;
			}
		}
		return valid;
	}

	private void validateNoteUI (string note)
	{
		bool valid = validateNote (note);
		ColorBlock cb = GameObject.Find ("CourseNoteUI").GetComponent<InputField> ().colors;
		if (!valid) {
			cb.highlightedColor = Color.red;
		} else {
			cb.highlightedColor = Color.white;
		}
		GameObject.Find ("CourseNoteUI").GetComponent<InputField> ().colors = cb;
	}

	private void saveCourseChanges(){
		if (validateNote (cursoNota.text)) {
			db.saveModifiedCourse (courseCode, cursoAprobado.isOn, float.Parse (cursoNota.text));
		}
	}

	public void moreInfo(){
		if(cursoEnlace != "null" ){
			print (cursoEnlace);
			Application.OpenURL (cursoEnlace);
		} else{
			//Somting
		}

	}
}