using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using TMPro;

public class MindLightModel : MonoBehaviour,
							  MindLightModel.Setter
{

	public interface Setter {

		void StartMindLight(MindFieldData data);

	}

	[Header("--------- Main Panels ---------")]
	[SerializeField]
	private GameObject m_panelQuestion;
	[SerializeField]
	private GameObject m_panelAnswer;

	[Header("--------- Question ---------")]
	[SerializeField]
	private TextMeshProUGUI m_textQuestion;

	[Header("--------- Choices ---------")]
	[SerializeField]
	private TMPMindFieldChoice[] m_textChoices;

	[Header("--------- Answer ---------")]
	[SerializeField]
	private TMPShadowed m_shadowedTitle;
	[SerializeField]
	private TextMeshProUGUI m_textAnswerLetter;
	[SerializeField]
	private TextMeshProUGUI m_textAnswerExplanation;

	[Header("--------- ANIMATION ---------")]
	[SerializeField]
	private Animator m_animatorBulb;
	[SerializeField]
	private string m_animParamBoolWrong;
	[SerializeField]
	private string m_animParamBoolCorrect;

	private void Awake() {
		if(m_animatorBulb == null) {
			LogUtil.PrintWarning(this.gameObject, this.GetType(), "Awake(): Animator for Bulb is missing.");
		}

		if(StringUtil.IsNullOrEmpty(m_animParamBoolCorrect) || StringUtil.IsNullOrEmpty(m_animParamBoolWrong)) {
			LogUtil.PrintWarning(this.gameObject, this.GetType(), "Awake(): Animation Bool Param/s missing");
		}
	}

	private void OnEnable() {
		for(int x=0; x<m_textChoices.Length; x++) {
			m_textChoices[x].GetIsCorrectAnswerClicked()
				.Subscribe(isCorrect => CheckAnswer(isCorrect))
				.AddTo(this);
		}
	}

	private void Start() {
		DeactivateBulbAnimation();
	}

	private void PrepareDialogs(MindFieldData data) {
		m_textQuestion.text = data.m_question;
		m_textAnswerExplanation.text = data.m_explanation;

		data.ShuffleChoices();

		for(int x=0; x<m_textChoices.Length; x++) {
			MindFieldChoice currentChoice = data.m_choices[x];
			m_textChoices[x].SetChoice(currentChoice);
			m_textChoices[x].SetReward(data.m_reward);

			if(currentChoice.m_isCorrect) {
				m_textAnswerLetter.text = m_textChoices[x].GetChoiceLetter();
			}
		}
	}

	private void CheckAnswer(bool isCorrect) {
		m_shadowedTitle.SetShadowedText(isCorrect ? 
			TheExplorersSpiels.HEADER_ANSWER_CORRECT 
			: TheExplorersSpiels.HEADER_ANSWER_WRONG);

		ActivatePanelAnswer();
		ActivateBulbAnimation(isCorrect);
	}

	private void ActivatePanelQuestion() {
		m_panelQuestion.SetActive(true);
		m_panelAnswer.SetActive(false);
	}

	private void ActivatePanelAnswer() {
		m_panelQuestion.SetActive(false);
		m_panelAnswer.SetActive(true);
	}

	private void ActivateBulbAnimation(bool isAnswerCorrect) {
		string paramToActivate = isAnswerCorrect ? m_animParamBoolCorrect : m_animParamBoolWrong;

		m_animatorBulb.SetBool(paramToActivate, true);
	}

	private void DeactivateBulbAnimation() {
		m_animatorBulb.SetBool(m_animParamBoolCorrect, false);
		m_animatorBulb.SetBool(m_animParamBoolWrong, false);
	}

	/* MindLightModel_Setter ---------------------------------------------------------------------------------------- */

	public void StartMindLight(MindFieldData data) {
		PrepareDialogs(data);
		ActivatePanelQuestion();

		DeactivateBulbAnimation();
	}

}//end of MindLightModel