using UnityEngine;

[CreateAssetMenu(fileName = "New MindFieldData", menuName = "MindField/Create MindField Data")]
public class MindFieldData : ScriptableObject {

	[Header("Q & A")]

	[TextArea]
	public string m_question;
	public MindFieldChoice[] m_choices = new MindFieldChoice[4];
	[TextArea]
	public string m_explanation;

	[Header("Reward")]
	public MindFieldReward m_reward;

	//Knuth shuffle
	public void ShuffleChoices() {
		int choiceLength = m_choices.Length;

		for(int x=0; x<choiceLength; x++) {
			MindFieldChoice currentChoice = m_choices[x];
			int randomIndex = Random.Range(0, choiceLength);

			m_choices[x] = m_choices[randomIndex];
			m_choices[randomIndex] = currentChoice;
		}
	}


}
