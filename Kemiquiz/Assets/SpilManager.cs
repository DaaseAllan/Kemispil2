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

    public int scoreRød;
    public int scoreBlå;

    [SerializeField]
    public Text rødScore;
    [SerializeField]
    public Text blåScore;
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
	private float delayBetweenQuestions;

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

    public void updateScore(string vinder)
    {
        if (vinder == "rød")
            scoreRød++;
            rødScore.text = "Rød score: " + scoreRød;
        if (vinder == "blå")
            scoreBlå++;
            blåScore.text = "Blå score: " + scoreBlå;

        return;
    }

    void Update()
    {
        //Debug.Log (Input.inputString);
        //Debug.Log (Blå1char.ToString ());
        if (Input.inputString == Blå1char.ToString().ToLower()) {
            if (BlåRigtigtSvar == 1 && FørsteSvar == true) {
                Debug.Log("Blå vinder");
                updateScore("blå");
            } else if (BlåRigtigtSvar == 2 && FørsteSvar == true) {
                Debug.Log("Rød vinder");
                updateScore("rød");
            }
        } else if (Input.inputString == Blå2char.ToString().ToLower()) {
            if (BlåRigtigtSvar == 1 && FørsteSvar == true) {
                Debug.Log("Rød vinder");
                updateScore("rød");
            } else if (BlåRigtigtSvar == 2 && FørsteSvar == true) {
                Debug.Log("Blå vinder");
                updateScore("blå");
            }
        } else if (Input.inputString == Rød1char.ToString().ToLower()) {
            if (RødRigtigtSvar == 1 && FørsteSvar == true) {
                Debug.Log("Rød vinder");
                updateScore("rød");
            } else if (RødRigtigtSvar == 2 && FørsteSvar == true) {
                Debug.Log("Blå vinder");
                updateScore("blå");
            }
        } else if (Input.inputString == Rød2char.ToString().ToLower()) {
            if (RødRigtigtSvar == 1 && FørsteSvar == true) {
                Debug.Log("Blå vinder");
                updateScore("blå");
            } else if (RødRigtigtSvar == 2 && FørsteSvar == true) {
                Debug.Log("Rød vinder");
                updateScore("rød");
            }
        }
    }
}
