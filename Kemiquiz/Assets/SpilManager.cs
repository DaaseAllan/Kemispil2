using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpilManager : MonoBehaviour {

	public Question[] questions;
	private static List<Question> unansweredQuestions;

	private Question currentQuestion;

	void Start()
	{
		Debug.Log ("Startet");
		if (unansweredQuestions == null || unansweredQuestions.Count == 0) {
			Debug.Log ("flytter array over i liste");
			unansweredQuestions = questions.ToList<Question> ();
		}

		getRandomQuestion ();
		Debug.Log (currentQuestion.questionName+ " " + currentQuestion.svar);

	}
	void getRandomQuestion(){
		int randomQuestionNummer = Random.Range (0, unansweredQuestions.Count);
		currentQuestion = unansweredQuestions [randomQuestionNummer];

		unansweredQuestions.RemoveAt (randomQuestionNummer);
	}
}