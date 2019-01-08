using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class GamePlay_PanelModel : MonoBehaviour,
									GamePlay_PanelModel.Setter,
									GamePlay_PanelModel.Freezer
{
	public interface Setter {

		void ShowPause();

		void ShowHUD();

		void ShowGameOver();

		void ShowLevelClear();

		void ShowLoading();

		void ShowMindLight();

	}

	public interface Freezer {

		void Freeze();

		void Unfreeze();

	}

	[Header("Controllable Panels")]
	[SerializeField]
	private GameObject m_panelHUD;
	[SerializeField]
	private GameObject m_panelPause;

	[Header("Uninterrupted Panels")]
	[SerializeField]
	private GameObject m_panelLoading;
	[SerializeField]
	private GameObject m_panelMindLight;
	[SerializeField]
	private GameObject m_panelLevelEnd;

	[Space]
	[SerializeField]
	private TMPShadowed m_shadowedLevelEndHeader;

	[Header("Buttons")]
	[SerializeField]
	private Button m_buttonPause;
	[SerializeField]
	private Button m_buttonResume;
	[SerializeField]
	private Button m_buttonReloadLevel;
	[SerializeField]
	private Button[] m_buttonsMainMenu;

	private bool isGameOver;
	private bool isMindLightInPlay;

	private void Awake() {
		if(ArePanelsIncomplete()) {
			LogUtil.PrintError(this.gameObject, this.GetType(), "Awake(): Incomplete Panel requirements.");
		}

		isGameOver = false;
		isMindLightInPlay = false;
	}

	private void Start() {
		ShowHUD();
	}

	private bool ArePanelsIncomplete() {
		return (m_panelLoading == null || m_panelMindLight == null || 
			m_panelHUD == null || m_panelPause == null || m_panelLevelEnd == null);
	}

	private void HideAllPanels() {
		m_panelPause.SetActive(false);
		m_panelHUD.SetActive(false);
		m_panelLevelEnd.SetActive(false);

		m_panelLoading.SetActive(false);
		m_panelMindLight.gameObject.SetActive(false);
	}

	private void ShowPanel(GameObject panel, bool isGameFrozen) {
		HideAllPanels();
		panel.SetActive(true);

		if(isGameFrozen) {
			Freeze();
		}
		else {
			Unfreeze();
		}
	}

	private void ShowEndGamePanel(bool isWin) {
		isGameOver = true;

		m_shadowedLevelEndHeader.SetShadowedText(isWin ? 
			TheExplorersSpiels.LEVEL_END_WIN : TheExplorersSpiels.LEVEL_END_LOSE);
		ShowPanel(m_panelLevelEnd, true);
	}

	/* Setter ---------------------------------------------------------------------------------------- */

	public void ShowPause() {
		if(isGameOver || isMindLightInPlay) {
			LogUtil.PrintWarning(this.gameObject, this.GetType(), "ShowPause(): Cannot show Pause panel. " +
				"Either game is over OR MindLight is in play.");
		}
		else {
			ShowPanel(m_panelPause, true);
		}
	}

	public void ShowHUD() {
		isMindLightInPlay = false;
		ShowPanel(m_panelHUD, false);
	}

	public void ShowGameOver() {
		ShowEndGamePanel(false);
	}

	public void ShowLevelClear() {
		ShowEndGamePanel(true);
	}
		
	public void ShowLoading() {
		isGameOver = true;
		ShowPanel(m_panelLoading, true);
	}

	public void ShowMindLight() {
		isMindLightInPlay = true;
		ShowPanel(m_panelMindLight, true);
	}

	/* Freezer ---------------------------------------------------------------------------------------- */

	public void Freeze() {
		Time.timeScale = 0f;
	}

	public void Unfreeze() {
		Time.timeScale = 1f;
	}

}//end of class GamePlay_PanelModel