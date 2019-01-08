using UnityEngine;

public abstract class KillableBehaviour : MonoBehaviour {

	public bool isDead;

	public abstract void DisableMovement();
	public abstract void EnableMovement();

	public abstract void HitOnce();
	public abstract void KillOnce();

	private void Awake() {
		isDead = false;
	}

}
