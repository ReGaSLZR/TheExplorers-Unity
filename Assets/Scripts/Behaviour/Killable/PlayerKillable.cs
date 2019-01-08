using System.Collections;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerKillable : KillableBehaviour
{

//	[SerializeField] private string animationFreezeMovement;
//	[SerializeField] private AudioClip[] clipsFreeze;

//	[Space]
	//NOTE: the usage of Animation Triggers on "Any State" animation transition is for one-time execution of those transitions
//	[SerializeField] private string animTrigger; 
//	private int animTriggerId;
	//NOTE: the usage of boolean Animation Parameter is for properly ending the animation state at any given moment
//	[Tooltip("Boolean animation parameter")]
	[SerializeField] private string animHit;
	[SerializeField] private AudioClip[] clipsHit;

	[Space]

	[SerializeField] private string animDeath;
	[SerializeField] private AudioClip[] clipsDead;
	[SerializeField] private float delayDestroyOnDeath = 1f;

	private Animator _animator;
	private AudioSource _audioSource;
	private Rigidbody2D _rigidbody2D;

	[Inject] PlayerStatsModel.Getter playerStatsGetter;
	[Inject] PlayerStatsModel.Setter playerStatsSetter;
	[Inject] GamePlay_PanelModel.Setter panelSwitcher;
	[Inject] BGMModel.Setter bgmSetter;

	private void Awake() {
		_animator = GetComponent<Animator>();
		_audioSource = GetComponent<AudioSource>();
		_rigidbody2D = GetComponent<Rigidbody2D>();

//		if(StringUtil.IsNonNullNonEmpty(animTrigger)) {
//			animTriggerId = Animator.StringToHash(animTrigger);
//		}
	}

	private void OnEnable() {
		playerStatsGetter.IsHit()
			.Subscribe(isHit => ApplyHit(isHit))
			.AddTo(this);

		playerStatsGetter.IsDead()
			.Where(isFinalHit => isFinalHit)
			.Subscribe(_ => {
				StopAllCoroutines();
				StartCoroutine(CorApplyDeath());

				panelSwitcher.ShowGameOver();
				bgmSetter.Pause();
			})
			.AddTo(this);
	}

	private void ApplyHit(bool isHit) {
		if(!playerStatsGetter.IsDead().Value) { //ignore Hit animation if Player has been hit for the FINAL time (gameOver)
//			if(isHit) {
//				_animator.SetTrigger(animTriggerId);
//			}
				
			_animator.SetBool(animHit, isHit);

			if(isHit) {
				AudioUtil.PlayRandomClip(GetType(), clipsHit, _audioSource);
			}
		}
	}

	private IEnumerator CorApplyDeath() {
		_animator.SetBool(animHit, false);
		_audioSource.Stop();

		_animator.SetBool(animDeath, true);
		AudioUtil.PlayRandomClip(GetType(), clipsDead, _audioSource);

		yield return new WaitForSeconds(delayDestroyOnDeath);
		Destroy(this.gameObject);
	}

	public override void DisableMovement() {
		_rigidbody2D.bodyType = RigidbodyType2D.Static;

		if(StringUtil.IsNonNullNonEmpty(animHit)) {
			_animator.SetBool(animHit, true);
			AudioUtil.PlayRandomClip(GetType(), clipsHit, _audioSource);
		}
	}

	public override void EnableMovement() {
		_rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

		if(StringUtil.IsNonNullNonEmpty(animHit)) {
			_animator.SetBool(animHit, false);
		}
	}

	public override void HitOnce() {
		playerStatsSetter.DeductHealth();
	}

	public override void KillOnce() {
		HitOnce(); //TODO note: no actual KillOnce() codes for now...
	}

}

