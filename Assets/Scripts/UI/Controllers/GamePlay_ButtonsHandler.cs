using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class GamePlay_ButtonsHandler : MonoBehaviour
{

	[Inject]
	GamePlay_PanelModel.Setter m_panelSwitcher;

	[Inject]
	SceneModel.Setter m_sceneModel;

	[Inject]
	BGMModel.Setter m_bgmModel;

	[Inject]
	CutsceneModel.Getter m_cutsceneModel;

	[Inject]
	PlayerInputModel m_playerInputModel;

	[Header("Buttons")]
	[SerializeField]
	private Button m_buttonPause;
	[SerializeField]
	private Button m_buttonResume;
	[SerializeField]
	private Button m_buttonFinishMindLight;
	[SerializeField]
	private Button m_buttonReloadLevel;
	[SerializeField]
	private Button[] m_buttonsMainMenu;

	private bool isPauseResumeCommander;

	private void Awake() {
		if(AreButtonsIncomplete()) {
			LogUtil.PrintError(this.gameObject, this.GetType(), "Awake(): Incomplete Button requirements.");
		}

		isPauseResumeCommander = false;
	}

	private void OnEnable() {
		m_playerInputModel.rIsPaused
			.Subscribe(isPaused => {
				//if this script commanded the change in playerInputMode.rIsPaused, 
				//no need to make changes as they have been applied through the button pressed
				if(!isPauseResumeCommander) {
					if(isPaused) {
						m_panelSwitcher.ShowPause();
					}
					else {
						m_panelSwitcher.ShowHUD();
					}
				}

				isPauseResumeCommander = false;
			})
			.AddTo(this);

		if(m_buttonPause != null) {
			m_buttonPause.OnPointerClickAsObservable()
				.Where(_ => m_cutsceneModel.GetIsDone().Value)
				.Subscribe(_ => {
					isPauseResumeCommander = true;
					m_playerInputModel.SetIsPaused(true);

					m_panelSwitcher.ShowPause();
				})
				.AddTo(this);
		}

		if(m_buttonResume != null) {
			m_buttonResume.OnPointerClickAsObservable()
				.Subscribe(_ => {
					isPauseResumeCommander = true;
					m_playerInputModel.SetIsPaused(false);

					m_panelSwitcher.ShowHUD();
				})
				.AddTo(this);
		}

		if(m_buttonFinishMindLight != null) {
			m_buttonFinishMindLight.OnPointerClickAsObservable()
				.Subscribe(_ => m_panelSwitcher.ShowHUD())
				.AddTo(this);
		}

		if(m_buttonReloadLevel != null) {
			m_buttonReloadLevel.OnPointerClickAsObservable()
				.Subscribe(_ => {
					ShowLoading();	
					m_sceneModel.ReloadCurrentScene();
				})
				.AddTo(this);
		}

		for(int x=0; x<m_buttonsMainMenu.Length; x++) {
			m_buttonsMainMenu[x].OnPointerClickAsObservable()
				.Subscribe(_ => {
					ShowLoading();
					m_sceneModel.ExitToMainMenu();
				})
				.AddTo(this);
		}
	}

	private bool AreButtonsIncomplete() {
		return (m_buttonPause == null || m_buttonResume == null || m_buttonFinishMindLight == null);
	}

	private void ShowLoading() {
		m_panelSwitcher.ShowLoading();	
		m_bgmModel.Pause();
	}

}
