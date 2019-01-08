using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class PlayerSkillsAI : AIBehaviour	
{

	[SerializeField] private SkillBehaviour skillBasicAttack;
	[SerializeField] private float basicAttack_Interval = 0.5f;
	private System.DateTimeOffset _lastBasicAttack;

	[Inject] PlayerStatsModel.Getter playerStatsGetter;
	[Inject] PlayerStatsModel.Setter playerStatsSetter;
	[Inject] PlayerInputModel playerInput;

	private void Awake() {
		if((skillBasicAttack == null)) {
			LogUtil.PrintError(gameObject, GetType(), "Incomplete SkillBehaviour parameters supplied. Destroying...");
			Destroy(this);
		}
	}

	private void Start () {
		if(skillBasicAttack != null) {
			this.FixedUpdateAsObservable()
				.Select(_ => playerInput.hasAttacked)
				.Where(hasPressedKey => (hasPressedKey))
				.Timestamp()
				.Where(x => x.Timestamp > _lastBasicAttack.AddSeconds(basicAttack_Interval))
				.Subscribe(x => {
					if(playerStatsSetter.DeductMindLight()) {
						_lastBasicAttack = x.Timestamp;
						skillBasicAttack.UseSkill(false);
					}
				})
				.AddTo(this);
		}

	}

}
