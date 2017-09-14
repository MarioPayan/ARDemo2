using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizMessages : MonoBehaviour {

	private GameObject quizInfo;

	public void continueButton(){
		quizInfo = QuizInfo.quizInfo;
		quizInfo.SendMessage ("nextQuiz");
	}

	public void tryAgainButton(){
		quizInfo = QuizInfo.quizInfo;
		quizInfo.SendMessage ("tryAgain");
	}

	public void exitButton(){
		SceneLoader sl = new SceneLoader ();
		sl.LoadNewSceneGO ("Menu");
	}

	public void sendAnswer1(){
		quizInfo = QuizInfo.quizInfo;
		quizInfo.SendMessage ("answerSelected", 1);
	}

	public void sendAnswer2(){
		quizInfo = QuizInfo.quizInfo;
		quizInfo.SendMessage ("answerSelected", 2);
	}

	public void sendAnswer3(){
		quizInfo = QuizInfo.quizInfo;
		quizInfo.SendMessage ("answerSelected", 3);
	}

	public void sendAnswer4(){
		quizInfo = QuizInfo.quizInfo;
		quizInfo.SendMessage ("answerSelected", 4);
	}
}
