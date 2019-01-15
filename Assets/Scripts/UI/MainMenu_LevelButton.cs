using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class MainMenu_LevelButton : MonoBehaviour {

	[Inject]
	SFXModel.Setter m_sfxModel;

	[Inject]
	SceneModel.Setter m_sceneModel;

	[Inject]
	MainMenu_PanelModel.Setter m_buttonsHandler;

	[SerializeField]
	private int m_sceneIndex = 1;
	[SerializeField]
	private Button m_button;
	[SerializeField]
	private AudioClip m_clip;

	private bool m_isLoading;

	private void Awake() {
		if(m_button == null) {
			LogUtil.PrintError(this.gameObject, this.GetType(), "Awake(): Button is NULL.");
		}

		m_isLoading = false;
	}

	private void OnEnable() {
		m_button.OnPointerUpAsObservable()
			.Where(_ => !m_isLoading)
			.Subscribe(_ => {
				m_isLoading = true;

				m_sfxModel.PlaySFX(m_clip);
				m_buttonsHandler.DisableAllElements();
				m_sceneModel.LoadScene(m_sceneIndex);
			})
			.AddTo(this);
	}

}
