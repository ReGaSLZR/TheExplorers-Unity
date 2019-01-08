using System;
using UnityEngine;
using UniRx;

public abstract class PlayerInputModel : MonoBehaviour
{

	[SerializeField] protected float movementBaseSpeed = 0.1f;


	public bool hasAttacked {protected set; get;}

	public bool hasJumped {protected set; get;}

	public ReactiveProperty<bool> rIsPaused {protected set; get;}

	public ReactiveProperty<bool> rIsDisabled {protected set; get;}

	public float movement {protected set; get;}

	public bool hasScreenButtons {protected set; get;}

	public PlayerInputModel() {
		rIsPaused = new ReactiveProperty<bool>(false);
		rIsDisabled = new ReactiveProperty<bool>(false);
	}

	private void Awake() {
		rIsDisabled.Value = false;
	}

	protected bool IsInputEnabled() {
		return (!rIsDisabled.Value);
	}

	public void SetIsDisabled(bool value) {
		rIsDisabled.Value = value;
	}

	public void SetIsPaused(bool value) {
		rIsPaused.Value = value;
	}

}
		