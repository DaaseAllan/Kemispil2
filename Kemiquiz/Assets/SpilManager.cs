using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class SpilManager : MonoBehaviour {

	public Question[] questions;
	private static List<Question> unansweredQuestions;

	private Question currentQuestion;
	string alleBogstaver = "ABCDEFGHIJKLMNOPGRSTUVWXYZ";
	List<int> BrugteChar = new List<int>();

	int BlåRigtigtSvar;
	int RødRigtigtSvar;
	bool FørsteSvar = true;

	public char Rød1char;
	public char Rød2char;
	public char Blå1char;
	public char Blå2char;



	[SerializeField]
	private Text questiontext;
	[SerializeField]
	private Text Rød1Tekst;
	[SerializeField]
	private Text Rød2Tekst;
	[SerializeField]
	private Text Blue1Tekst;
	[SerializeField]
	private Text Blue2Tekst;

	//Chartekst
	[SerializeField]
	private Text Rød1CharObject;
	[SerializeField]
	private Text Rød2CharObject;
	[SerializeField]
	private Text Blå1CharObject;
	[SerializeField]
	private Text Blå2CharObject;


	[SerializeField]
	private Text Rød1AnswerText;

	[SerializeField]
	private Text Rød2AnswerText;

	[SerializeField]
	private Text Blå1AnswerText;

	[SerializeField]
	private Text Blå2AnswerText;

	[SerializeField]
	private float delayBetweenQuestions;

	[SerializeField]
	private Animator animator;

	void Start()
	{
		//Laver array om til liste
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

		//Sætter strinsne ind i kasserne.

		questiontext.text = currentQuestion.questionName;
		int etllerto = Random.Range (1, 3);
		if (etllerto == 1) {
			Rød1Tekst.text = currentQuestion.mulighed1;
			Rød2Tekst.text = currentQuestion.mulighed2;
			RødRigtigtSvar = 1;

		} else {
			Rød1Tekst.text = currentQuestion.mulighed2;
			Rød2Tekst.text = currentQuestion.mulighed1;
			RødRigtigtSvar = 2;

		}
			
		etllerto = Random.Range (1, 3);
		if (etllerto == 1) {
			Blue1Tekst.text = currentQuestion.mulighed1;
			Blue2Tekst.text = currentQuestion.mulighed2;
			BlåRigtigtSvar = 1;

		} else {
			Blue1Tekst.text = currentQuestion.mulighed2;
			Blue2Tekst.text = currentQuestion.mulighed1;
			BlåRigtigtSvar = 2;

		}

		//Generator chars til de forskellige knapper og husker dem.
		Rød1char = GenerateBogstav();
		Rød1CharObject.text = Rød1char.ToString();

		Rød2char = GenerateBogstav();
		Rød2CharObject.text = Rød2char.ToString();

		Blå1char = GenerateBogstav();
		Blå1CharObject.text = Blå1char.ToString();

		Blå2char = GenerateBogstav();
		Blå2CharObject.text = Blå2char.ToString();

		//Fjerner spørgsmålet fra listen over ubrugete spørgsmål.
		unansweredQuestions.RemoveAt (randomQuestionNummer);
	}

	IEnumerator TransitionTilNextQuestion ()
	{
		//Fjerner spørgsmålet fra listen over ubrugete spørgsmål.
		unansweredQuestions.Remove(currentQuestion);

		yield return new WaitForSeconds (delayBetweenQuestions);

		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public void Player1Svar(int svar){

		Debug.Log (svar);

	}

	public void Player2Svar(int svar){

	}

	public char GenerateBogstav()
	{
		char c;
			c = alleBogstaver [Random.Range (1, alleBogstaver.Length)];

			//Tjekker forhåbentligt om en char er brugt.
		while (BrugteChar.Contains (c) == true) {
				c = alleBogstaver [Random.Range (1, alleBogstaver.Length)];

			} 
				BrugteChar.Add (c);
				return c;
			
		


	}
	void Update ()
	{
		//Debug.Log (Input.inputString);
		//Debug.Log (Blå1char.ToString ());
		if (Input.inputString == Blå1char.ToString ().ToLower ()) {
			if (BlåRigtigtSvar == 1 && FørsteSvar == true) {
				Answertext ("Blå1", 1);
			} else if (BlåRigtigtSvar == 2 && FørsteSvar == true) {
				Answertext ("Blå1", 0);
			}
		} else if (Input.inputString == Blå2char.ToString ().ToLower ()) {
			if (BlåRigtigtSvar == 1 && FørsteSvar == true) {
				Answertext ("Blå2", 0);
			} else if (BlåRigtigtSvar == 2 && FørsteSvar == true) {
				Answertext ("Blå2", 1);
			}
		} else if (Input.inputString == Rød1char.ToString ().ToLower ()) {
			if (RødRigtigtSvar == 1 && FørsteSvar == true) {
				Answertext ("Rød1", 1);
			} else if (RødRigtigtSvar == 2 && FørsteSvar == true) {
				Answertext ("Rød1", 0);
			} 
		} else if (Input.inputString == Rød2char.ToString ().ToLower ()) {
			if (RødRigtigtSvar == 1 && FørsteSvar == true) {
				Answertext ("Rød2", 0);
			} else if (RødRigtigtSvar == 2 && FørsteSvar == true) {
				Answertext ("Rød2", 1);
			}
	}
}

	void Answertext(string knapnavn, int rigtigt){



		switch (knapnavn) {
		case"Blå1":
			Blå1AnswerText.enabled = true;
			Blå2AnswerText.enabled = false;
			Rød1AnswerText.enabled = false;
			Rød2AnswerText.enabled = false;
			animator.SetTrigger ("Blå1");
			if (rigtigt == 1) {
				
				Blå1AnswerText.text = "Rigtigt";

			} else {
				Blå1AnswerText.text = "Forkert";
			}
			break;

		case"Blå2":
			Blå1AnswerText.enabled = false;
			Blå2AnswerText.enabled = true;
			Rød1AnswerText.enabled = false;
			Rød2AnswerText.enabled = false;
			animator.SetTrigger ("Blå2");
			if (rigtigt == 1) {

				Blå2AnswerText.text = "Rigtigt";

			} else {
				Blå2AnswerText.text = "Forkert";
			}
			break;

		case"Rød1":
			Blå1AnswerText.enabled = false;
			Blå2AnswerText.enabled = false;
			Rød1AnswerText.enabled = true;
			Rød2AnswerText.enabled = false;
			animator.SetTrigger ("Rød1");
			if (rigtigt == 1) {

				Rød1AnswerText.text = "Rigtigt";

			} else {
				Rød1AnswerText.text = "Forkert";
			}
			break;

		case"Rød2":
			Blå1AnswerText.enabled = false;
			Blå2AnswerText.enabled = false;
			Rød1AnswerText.enabled = false;
			Rød2AnswerText.enabled = true;
			animator.SetTrigger ("Rød2");
			if (rigtigt == 1) {

				Rød2AnswerText.text = "Rigtigt";

			} else {
				Rød2AnswerText.text = "Forkert";
			}
			break;
		}

	}
}