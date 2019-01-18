using UnityEngine;
using TMPro;

public class MainMenu_ScoreDisplay : MonoBehaviour {

	[SerializeField]
	private TextMeshProUGUI[] m_scoreDisplayAscending;

	private void Awake() {
		if((m_scoreDisplayAscending == null) || m_scoreDisplayAscending.Length == 0) {
			LogUtil.PrintError(this.gameObject, this.GetType(), "Awake(): Missing array of SCORE display UI elements.");
			return;
		}

		ConfigureScoreDisplay();
	}

	private void ConfigureScoreDisplay() {
		string[] scores = PlayerPrefsUtil.GetHighScores();

		for(int x=0; x<scores.Length; x++) {
			if(m_scoreDisplayAscending[x] != null) {
				m_scoreDisplayAscending[x].text = (x+1) + ". " + scores[x];
			}
			else {
				LogUtil.PrintWarning(this.gameObject, this.GetType(), 
					"ConfigureScoreDisplay(): Score Display element at index " + x + " is missing.");
			}
		}
	}

}
