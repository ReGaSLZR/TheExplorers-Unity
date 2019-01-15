using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using TMPro;
using Zenject;

public class UIStats : MonoBehaviour
{

	[Inject]
	PlayerStatsModel.Getter m_playerStats;

	[Header("Health")]
	[SerializeField]
	private Slider m_sliderHealth;
	[SerializeField]
	private TextMeshProUGUI[] m_textHealth;

	[Header("MindLight")]
	[SerializeField]
	private Slider m_sliderMindLight;
	[SerializeField]
	private TextMeshProUGUI[] m_textMindLight;

	[Header("Score")]
	[SerializeField]
	private TextMeshProUGUI[] m_textScore;

	private void OnEnable() {
		m_playerStats.GetHealth()
			.Subscribe(health => {
				ReflectValueToText(health, m_textHealth, "HEALTH");
				ReflectValueToSlider(health, m_sliderHealth);
			})
			.AddTo(this);

		m_playerStats.GetMindlight()
			.Subscribe(mindLight => {
				ReflectValueToText(mindLight, m_textMindLight, "MIND LIGHT");
				ReflectValueToSlider(mindLight, m_sliderMindLight);
			})
			.AddTo(this);

		m_playerStats.GetScore()
			.Subscribe(score => ReflectValueToText(score, m_textScore, "SCORE"))
			.AddTo(this);

	}

	private void ReflectValueToSlider(int value, Slider slider) {
		if(slider != null) {
			slider.value = value;	
		}
	}

	private void ReflectValueToText(int value, TextMeshProUGUI[] arrayText, string arrayName) {
		for(int x=0; x<arrayText.Length; x++) {
			if(arrayText[x] != null) {
				arrayText[x].text = value.ToString();
			} 
			else {
				LogUtil.PrintWarning(this.gameObject, this.GetType(), 
					"OnEnable(): array " + arrayName + " @ index " + x + " is NULL.");	
			}
		}
	}

}

