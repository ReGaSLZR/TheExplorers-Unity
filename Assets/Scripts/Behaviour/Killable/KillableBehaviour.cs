using UnityEngine;
using UniRx;

public abstract class KillableBehaviour : MonoBehaviour {

	public ReactiveProperty<bool> isDead {protected set; get;}

	public abstract void DisableMovement();
	public abstract void EnableMovement();

	public abstract void HitOnce();
	public abstract void KillOnce();

	public KillableBehaviour() {
		isDead = new ReactiveProperty<bool>(false);
	}

}
