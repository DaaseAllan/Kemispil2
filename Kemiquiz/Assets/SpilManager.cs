using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpilManager : MonoBehaviour {

	public Question[] questions;
	private static List<Question> unansweredQuestions;

	private Question currentQuestion;

	void start()
	{
		Debug.Log ("Startet");
		if (unansweredQuestions == null || unansweredQuestions.Count == 0) {
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