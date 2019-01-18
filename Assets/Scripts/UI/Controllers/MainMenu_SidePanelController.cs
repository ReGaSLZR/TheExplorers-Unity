using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class MainMenu_SidePanelController : MonoBehaviour {

	[Header("All button objects at index 0 should be from the SIDE Panel for Buttons.")]

	[SerializeField]
	private Button[] m_buttonWelcomes;
	[SerializeField]
	private GameObject m_panelWelcome;

	[Space]

	[SerializeField]
	private Button[] m_buttonLevels;
	[SerializeField]
	private GameObject m_panelLevel;

	[Space]

	[SerializeField]
	private Button[] m_buttonControls;
	[SerializeField]
	private GameObject m_panelControls;

	[Space]

	[SerializeField]
	private Button[] m_buttonSettings;
	[SerializeField]
	private GameObject m_panelSettings;

	[Space]

	[SerializeField]
	private Button[] m_buttonScores;
	[SerializeField]
	private GameObject m_panelScore;

	private void OnEnable() {
		SetListeners(m_buttonWelcomes, m_panelWelcome);
		SetListeners(m_buttonLevels, m_panelLevel);
		SetListeners(m_buttonControls, m_panelControls);
		SetListeners(m_buttonSettings, m_panelSettings);
		SetListeners(m_buttonScores, m_panelScore);
	}

	private void Start() {
		DisableAllPanels();
		m_panelWelcome.SetActive(true);
	}

	private void SetListeners(Button[] buttons, GameObject panel) {
		for(int x=0; x<buttons.Length; x++) {
			if(buttons[x] != null) {
				buttons[x].OnClickAsObservable()
					.Subscribe(_ => {
						EnableAllSideButtons();
						DisableAllPanels();
						panel.SetActive(true);
						buttons[0].interactable = false;
					})
					.AddTo(this);
			}
			else {
				LogUtil.PrintInfo(this.gameObject, this.GetType(), "SetListeners(): A panel button is missing.");
			}
		}
	}

	private void DisableAllPanels() {
		m_panelWelcome.SetActive(false);
		m_panelLevel.SetActive(false);
		m_panelControls.SetActive(false);
		m_panelSettings.SetActive(false);
		m_panelScore.SetActive(false);
	}

	private void EnableAllSideButtons() {
		m_buttonWelcomes[0].interactable = true;
		m_buttonLevels[0].interactable = true;
		m_buttonControls[0].interactable = true;
		m_buttonSettings[0].interactable = true;
		m_buttonScores[0].interactable = true;
	}

}
