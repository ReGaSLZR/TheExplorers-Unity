using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using TMPro;
using Zenject;

public class TMPMindFieldChoice : MonoBehaviour {

	private static float CLICK_INTERVAL = 1f;

	[SerializeField]
	private Button m_button;
	[SerializeField]
	private TextMeshProUGUI m_choiceValue;
	[SerializeField]
	private TextMeshProUGUI m_choiceLetter;

	[Space]
	[SerializeField]
	private AudioClip m_sfxClick_Correct;
	[SerializeField]
	private AudioClip m_sfxClick_Wrong;

	[Inject]
	PlayerStatsModel.Setter m_playerStats;

	[Inject]
	SFXModel.Setter m_sfxModel;

	private ReactiveProperty<bool> m_isChoiceCorrect;

	private MindFieldChoice m_choice;
	private MindFieldReward m_reward;
	private DateTimeOffset m_lastClicked;

	public TMPMindFieldChoice() {
		m_isChoiceCorrect = new ReactiveProperty<bool>();
	}

	private void Awake() {
		if(m_button == null) {
			LogUtil.PrintError(this.gameObject, this.GetType(), "Awake(): Button is null.");
		}
	}

	private void OnEnable() {
		m_button.OnPointerDownAsObservable()
			.Timestamp()
			.Where(x => x.Timestamp > m_lastClicked.AddSeconds(CLICK_INTERVAL))
			.Subscribe(x => {
				if(m_choice.m_isCorrect) {
					m_playerStats.AddMindLight(m_reward.m_mindLight);
					m_playerStats.AddScore(m_reward.m_score);
				}

				m_sfxModel.PlaySFX(m_choice.m_isCorrect ? m_sfxClick_Correct : m_sfxClick_Wrong);
				m_isChoiceCorrect.SetValueAndForceNotify(m_choice.m_isCorrect);
				m_lastClicked = x.Timestamp;
			})
			.AddTo(this);
	}

	public void SetChoice(MindFieldChoice choice) {
		m_choice = choice;
		m_choiceValue.text = choice.m_choiceValue;
	}

	public void SetReward(MindFieldReward reward) {
		if(reward == null) {
			LogUtil.PrintError(this.gameObject, this.GetType(), "SetReward(): Whoops! Reward value is NULL.");
		}

		m_reward = reward;
	}
	
	public ReactiveProperty<bool> GetIsCorrectAnswerClicked() {
		return m_isChoiceCorrect;	
	}

	public string GetChoiceLetter() {
		return m_choiceLetter.text;
	}

	public MindFieldChoice GetMindFieldChoice() {
		return m_choice;
	}
		
}
