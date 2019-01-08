using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class LevelButton : MonoBehaviour {

	[Inject]
	SFXModel.Setter m_sfxModel;

	[Inject]
	SceneModel.Setter m_sceneModel;

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
	}

	private void OnEnable() {
		m_button.OnPointerUpAsObservable()
			.Where(_ => !m_isLoading)
			.Subscribe(_ => {
				m_button.interactable = false;
				m_isLoading = true;

				m_sfxModel.PlaySFX(m_clip);
				m_sceneModel.LoadScene(m_sceneIndex);

			})
			.AddTo(this);
	}

}
