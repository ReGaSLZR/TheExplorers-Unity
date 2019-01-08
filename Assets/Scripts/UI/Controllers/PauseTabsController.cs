using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PauseTabsController : MonoBehaviour {

	[SerializeField]
	private PauseTab m_tabStats;
	[SerializeField]
	private PauseTab m_tabControls;
	[SerializeField]
	private PauseTab m_tabVolume;

	private void OnEnable() {
		SetClickListeners();
	}

	private void Start() {
		ActivateTab(m_tabStats);
	}

	private void SetClickListeners() {
		m_tabStats.GetButton().OnPointerClickAsObservable()
			.Subscribe(_ => ActivateTab(m_tabStats))
			.AddTo(this);

		m_tabControls.GetButton().OnPointerClickAsObservable()
			.Subscribe(_ => ActivateTab(m_tabControls))
			.AddTo(this);

		m_tabVolume.GetButton().OnPointerClickAsObservable()
			.Subscribe(_ => ActivateTab(m_tabVolume))
			.AddTo(this);
	}

	private void ActivateTab(PauseTab tab) {
		DeactivateAllTabs();
		tab.SetTabActive(true);
	}

	private void DeactivateAllTabs() {
		m_tabStats.SetTabActive(false);
		m_tabControls.SetTabActive(false);
		m_tabVolume.SetTabActive(false);
	}

}
