using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class CutsceneTrigger : MonoBehaviour
{

	[Inject]
	CutsceneModel.Setter m_cutsceneModelSetter;

	[Inject]
	CutsceneModel.Getter m_cutsceneModelGetter;

	[Inject]
	PlayerInputModel m_playerInputModel;

	[SerializeField]
	private CutsceneLine[] m_lines;

	private bool isTriggered;

	private void Awake() {
		isTriggered = false;
	}

	private void OnEnable() {
		m_cutsceneModelGetter.GetIsDone()
			.Where(isDone => (isDone && isTriggered))
			.Subscribe(_ => {
				m_playerInputModel.SetIsDisabled(false);
				Destroy(this.gameObject);
			})
			.AddTo(this);

		this.OnCollisionEnter2DAsObservable()
			.Where(collision => (TagUtil.IsTagPlayer(collision.gameObject.tag) && (!isTriggered)))
			.Subscribe(_ => {
				isTriggered = true;

				LogUtil.PrintInfo(this.gameObject, this.GetType(), "OnTriggerEnter 2D: starting cutscene");
				m_cutsceneModelSetter.StartCutscene(m_lines);
				m_playerInputModel.SetIsDisabled(true);
			})
			.AddTo(this);
	}

}

