using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class MindLightTrigger : MonoBehaviour {

	[Inject]
	MindLightModel.Setter m_mindLight;

	[Inject]
	GamePlay_PanelModel.Setter m_panelSwitcher;

	[SerializeField]
	private MindFieldData m_data;

	private void Start() {
		this.OnTriggerEnter2DAsObservable()
			.Where(otherCollider2D => TagUtil.IsTagPlayer(otherCollider2D.tag))
			.Subscribe(_ => TriggerMindLight())
			.AddTo(this);
	}

	private void TriggerMindLight() {
		m_panelSwitcher.ShowMindLight();
		m_mindLight.StartMindLight(m_data);

		Destroy(this.gameObject);
	}

}
