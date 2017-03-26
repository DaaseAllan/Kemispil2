using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SpilManager : MonoBehaviour {

	public Question[] questions;
	private static List<Question> unansweredQuestions;

	private Question currentQuestion;
	string alleBogstaver = "ABCDEFGHIJKLMNOPGRSTUVWXYZ";
	List<int> BrugteChar = new List<int>();

	public char Rød1char;
	public char Rød2char;
	public char Blå1char;
	public char Blå2char;



	[SerializeField]
	private Text questiontext;
	[SerializeField]
	private Text Rød1CharObject;
	[SerializeField]
	private Text Rød2CharObject;
	[SerializeField]
	private Text Blå1CharObject;
	[SerializeField]
	private Text Blå2CharObject;

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

		questiontext.text = currentQuestion.questionName;

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


}