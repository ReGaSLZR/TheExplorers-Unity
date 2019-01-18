using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Zenject;

public abstract class LevelButton : MonoBehaviour {

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

	protected abstract void OnLevelButtonClick();

	private void Awake() {
		if(m_button == null) {
			LogUtil.PrintError(this.gameObject, this.GetType(), "Awake(): Button is NULL.");
		}

		m_isLoading = false;
	}

	protected void CheckIfUnlocked() {
		LogUtil.PrintInfo(this.gameObject, this.GetType(), "CheckIfUnlocked(): Latest Level cleared is: " + PlayerPrefsUtil.GetLatestLevel());
		m_button.interactable = (m_sceneIndex <= (PlayerPrefsUtil.GetLatestLevel() + 1));
	}

	protected void RegisterOnClickListener() {
		m_button.OnClickAsObservable()
			.Where(_ => !m_isLoading)
			.Subscribe(_ => {
				LogUtil.PrintInfo(this.gameObject, this.GetType(), 
					"OnEnable(): level button has been clicked. Loading scene " + m_sceneIndex);
				m_isLoading = true;

				m_sfxModel.PlaySFX(m_clip);

				OnLevelButtonClick();
				m_sceneModel.LoadScene(m_sceneIndex);
			})
			.AddTo(this);
	}

}
