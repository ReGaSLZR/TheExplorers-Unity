using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class ItemHealth : MonoBehaviour {

	[Inject]
	PlayerStatsModel.Setter m_playerModel;

	[Inject]
	SFXModel.Setter m_sfxModel;

	[SerializeField]
	private int m_healthValue = 1;
	[SerializeField]
	private float m_timerDestroyOnTake = 1f;
	[SerializeField]
	private AudioClip m_clipTaken;

	[Header("Children GameObjects")]
	[SerializeField]
	private GameObject m_childDefault;
	[SerializeField]
	private GameObject m_childTakenFX;

	private bool isConsumed = false;

	private void Awake() {
		if((m_childDefault == null) || (m_childTakenFX == null)) {
			LogUtil.PrintWarning(this.gameObject, this.GetType(), "Awake(): Missing Child/ren objects.");
		}
	}

	private void OnEnable() {
		this.OnTriggerEnter2DAsObservable()
			.Where(otherCollider2D => ((TagUtil.IsTagPlayer(otherCollider2D.gameObject.tag)) && !isConsumed))
			.Subscribe(_ => ConsumeItem())
			.AddTo(this);
	}

	private void Start() {
		ActivateDefault();
	}

	private void ConsumeItem() {
		m_playerModel.AddHealth(m_healthValue);
		m_sfxModel.PlaySFX(m_clipTaken);

		isConsumed = true;
		ActivateTakenFX();
		Destroy(this.gameObject, m_timerDestroyOnTake);
	}

	private void ActivateDefault() {
		m_childDefault.SetActive(true);
		m_childTakenFX.SetActive(false);
	}

	private void ActivateTakenFX() {
		m_childDefault.SetActive(false);
		m_childTakenFX.SetActive(true);
	}

}
