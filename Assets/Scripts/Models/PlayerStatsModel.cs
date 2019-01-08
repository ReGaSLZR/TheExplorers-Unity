using System.Collections;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerStatsModel : MonoBehaviour,
								PlayerStatsModel.Getter,
								PlayerStatsModel.Setter
{

	/* INTERFACES ---------------------------------------------------------------------------------------- */

	public interface Getter {

		ReactiveProperty<bool> IsDead();

		ReactiveProperty<bool> IsHit();

		ReactiveProperty<int> GetHealth();

		ReactiveProperty<int> GetMindlight();

		ReactiveProperty<int> GetScore();

	}

	public interface Setter {

		void AddHealth(int healthToAdd);

		void AddMindLight(int mindlightToAdd);

		void AddScore(int scoreToAdd);

		bool DeductHealth();

		bool DeductMindLight();

		void ResetStats();

	}

	/* ---------------------------------------------------------------------------------------- */

	private static float HIT_INTERVAL = 2f;
	private static float MINDLIGHT_INTERVAL = 1f;

	private ReactiveProperty<int> m_health;
	private ReactiveProperty<int> m_mindlight;
	private ReactiveProperty<int> m_score;

	//derived variables
	private ReactiveProperty<bool> m_isDead;
	private ReactiveProperty<bool> m_isHit;
	private ReactiveProperty<bool> m_isMindLightInUse;

	public PlayerStatsModel() {
		m_health = new ReactiveProperty<int>();
		m_mindlight = new ReactiveProperty<int>();
		m_score = new ReactiveProperty<int>();

		m_isDead = new ReactiveProperty<bool>();
		m_isHit = new ReactiveProperty<bool>();
		m_isMindLightInUse = new ReactiveProperty<bool>();

		ResetStats();
	}

	private void Start() {
		this.UpdateAsObservable()
			.Select(_ => m_health.Value)
			.Where(health => (health == 0))
			.Subscribe(_ => m_isDead.Value = true)
			.AddTo(this);
	}

	/* PlayerStatsModel_Getter ---------------------------------------------------------------------------------------- */

	public ReactiveProperty<bool> IsDead() {
		return m_isDead;
	}

	public ReactiveProperty<bool> IsHit() {
		return m_isHit;
	}

	public ReactiveProperty<int> GetHealth() {
		return m_health;
	}

	public ReactiveProperty<int> GetMindlight() {
		return m_mindlight;
	}

	public ReactiveProperty<int> GetScore() {
		return m_score;
	}

	/* PlayerStatsModel_Setter ---------------------------------------------------------------------------------------- */

	public void AddHealth(int healthToAdd) {
		LogUtil.PrintInfo(this.gameObject, this.GetType(), "AddHealth(): healthToAdd = " + healthToAdd);

		if(healthToAdd <= 0) {
			LogUtil.PrintWarning(this.gameObject, this.GetType(), "AddHealth(): invalid healthToAdd value.");
			return;
		}

		int tempNewHealth = (healthToAdd + m_health.Value);
		m_health.Value = (tempNewHealth > TheExplorersConfig.HEALTH_MAX) ? TheExplorersConfig.HEALTH_MAX : tempNewHealth;
	}

	public void AddMindLight(int mindlightToAdd) {
		LogUtil.PrintInfo(this.gameObject, this.GetType(), "AddMindLight(): mindlightToAdd = " + mindlightToAdd);

		if(mindlightToAdd <= 0) {
			LogUtil.PrintWarning(this.gameObject, this.GetType(), "AddMindLight(): invalid mindlightToAdd value.");
			return;
		}

		int tempNewMindLight = (mindlightToAdd + m_mindlight.Value);
		m_mindlight.Value = (tempNewMindLight > TheExplorersConfig.MINDLIGHT_MAX) ? TheExplorersConfig.MINDLIGHT_MAX : tempNewMindLight;
	}

	public void AddScore(int scoreToAdd) {
		LogUtil.PrintInfo(this.gameObject, this.GetType(), "AddScore(): scoreToAdd=" + scoreToAdd);

		if(scoreToAdd <= 0) {
			LogUtil.PrintWarning(this.gameObject, this.GetType(), "AddScore(): invalid scoreToAdd value.");
			return;
		}

		int tempNewScore = (scoreToAdd + m_score.Value);
		m_score.Value = (tempNewScore > TheExplorersConfig.SCORE_MAX) ? TheExplorersConfig.SCORE_MAX : tempNewScore;
	}

	public bool DeductHealth() {
		if(!m_isHit.Value && (m_health.Value > 0)) {
			StartCoroutine(CorApplyHit());
			return true;
		}
		else {
			LogUtil.PrintInfo(this.gameObject, this.GetType(), "DeductHealth(): " +
				"Cannot deduct. Interval in progress or there is no more health.");
			return false;
		}
	}

	public bool DeductMindLight() {
		if(!m_isMindLightInUse.Value && (m_mindlight.Value > 0)) {
			StartCoroutine(CorApplyMindLightUse());
			return true;
		}
		else {
			LogUtil.PrintInfo(this.gameObject, this.GetType(), "DeductMindLight(): " +
				"Cannot deduct. Interval in progress or there's no mindlight.");
			return false;
		}
	}

	public void ResetStats() {
		m_health.Value = TheExplorersConfig.HEALTH_STARTING;
		m_mindlight.Value = TheExplorersConfig.MINDLIGHT_STARTING;
		m_score.Value = TheExplorersConfig.SCORE_STARTING;

		m_isDead.Value = (m_health.Value == 0);
		m_isHit.Value = false;
		m_isMindLightInUse.Value = false;
	}

	private IEnumerator CorApplyHit() {
		m_health.Value -= TheExplorersConfig.HEALTH_INCREMENT;

		m_isHit.Value = true;
		yield return new WaitForSeconds(HIT_INTERVAL);
		m_isHit.Value = false;
	}

	private IEnumerator CorApplyMindLightUse() {
		m_mindlight.Value -= TheExplorersConfig.MINDLIGHT_INCREMENT;

		m_isMindLightInUse.Value = true;
		yield return new WaitForSeconds(MINDLIGHT_INTERVAL);
		m_isMindLightInUse.Value = false;
	}

} //end of class PlayerStatsModel