using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuizInfo : MonoBehaviour {

	public static GameObject quizInfo;
	public static GameObject correctAnswer;
	public static GameObject wrongAnswer;
	private QuizesDAO db;
	private Text question;
	private GameObject answer1;
	private GameObject answer2;
	private GameObject answer3;
	private GameObject answer4;
	private Text textAnswer1;
	private Text textAnswer2;
	private Text textAnswer3;
	private Text textAnswer4;
	private int answerNumber;
	private int questionNumber;

	void Start () {
		db = new QuizesDAO ();
		question = GameObject.Find ("QuestionUI").GetComponent<Text> ();
		answer1 = GameObject.Find ("Answer1UI");
		answer2 = GameObject.Find ("Answer2UI");
		answer3 = GameObject.Find ("Answer3UI");
		answer4 = GameObject.Find ("Answer4UI");
		textAnswer1 = GameObject.Find ("TextAnswer1UI").GetComponent<Text> ();
		textAnswer2 = GameObject.Find ("TextAnswer2UI").GetComponent<Text> ();
		textAnswer3 = GameObject.Find ("TextAnswer3UI").GetComponent<Text> ();
		textAnswer4 = GameObject.Find ("TextAnswer4UI").GetComponent<Text> ();
		answerNumber = 0;
		questionNumber = 0;
		generateQuiz ();
		correctAnswer.SetActive (false);
		wrongAnswer.SetActive (false);
	}
	
	void Update () {
		
	}

	void Awake ()
	{
		quizInfo = GameObject.Find ("QuizCanvas");
		correctAnswer = GameObject.Find ("CorrectAnswerCanvas");
		wrongAnswer = GameObject.Find ("WrongAnswerCanvas");
	}

	void generateQuiz(){
		int newQuestionNumber = 0;
		do {
			newQuestionNumber = UnityEngine.Random.Range (0, db.getCount ()) + 1;
		} while(questionNumber == newQuestionNumber);
		questionNumber = newQuestionNumber;
		string questionNumberString = (questionNumber).ToString();
		question.text = db.getQuestion (questionNumberString);
		answerNumber = db.getAnswerNumber (questionNumberString);
		if (answerNumber == 0) {
			answerNumber = 3;
			textAnswer3.text = "Continuar";
			answer1.SetActive (false);
			answer2.SetActive (false);
			answer4.SetActive (false);
		} else {
			answer1.SetActive (true);
			answer2.SetActive (true);
			answer4.SetActive (true);
			textAnswer1.text = db.getAnswer1 (questionNumberString);
			textAnswer2.text = db.getAnswer2 (questionNumberString);
			textAnswer3.text = db.getAnswer3 (questionNumberString);
			textAnswer4.text = db.getAnswer4 (questionNumberString);
			answerNumber = db.getAnswerNumber (questionNumberString);
		}
	}

	public void nextQuiz(){
		correctAnswer.SetActive (false);
		generateQuiz ();
	}

	public void tryAgain(){
		wrongAnswer.SetActive (false);
	}

	public void answerSelected(int answer){
		if (answer == answerNumber) {
			db.setResolved (questionNumber);
			correctAnswer.SetActive (true);
		} else {
			wrongAnswer.SetActive (true);
		}
	}
}
