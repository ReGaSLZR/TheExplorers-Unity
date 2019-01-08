using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerInputKeyboardMouse : PlayerInputModel 
{

	[SerializeField] private KeyCode attack = KeyCode.Space;
	[SerializeField] private KeyCode altAttack = KeyCode.E;

	[Space]

	[SerializeField] private KeyCode jump = KeyCode.W;
	[SerializeField] private KeyCode altJump = KeyCode.UpArrow;

	[Space]

	[SerializeField] private KeyCode moveRight = KeyCode.D;
	[SerializeField] private KeyCode altMoveRight = KeyCode.RightArrow;

	[Space]

	[SerializeField] private KeyCode moveLeft = KeyCode.A;
	[SerializeField] private KeyCode altMoveLeft = KeyCode.LeftArrow;

	[Space]

	[SerializeField] private KeyCode pause = KeyCode.Escape;

	private void OnEnable() {
		hasScreenButtons = false;

		SetUpMovementControls();
		SetUpJumpControls();
		SetUpAttackControls();

		SetUpPauseControls();
	}

	private void SetUpPauseControls() {
		this.FixedUpdateAsObservable()
			.Where(_ => IsInputEnabled())
			.Select(_ => Input.GetKeyDown(pause))
			.Where(isPressed => isPressed)
			.Subscribe(_ => rIsPaused.Value = !rIsPaused.Value)
			.AddTo(this);
	}

	private void SetUpJumpControls() {
		this.FixedUpdateAsObservable()
			.Where(_ => IsInputEnabled())
			.Select(_ => Input.GetKeyDown(jump) || Input.GetKeyDown(altJump))
			.Subscribe(isPressed => hasJumped = isPressed)
			.AddTo(this);
	}

	private void SetUpAttackControls() {
		this.FixedUpdateAsObservable()
			.Where(_ => IsInputEnabled())
			.Select(_ => Input.GetKeyDown(attack) || Input.GetKeyDown(altAttack))
			.Subscribe(isPressed => hasAttacked = isPressed)
			.AddTo(this);
	}

	private void SetUpMovementControls() {
		this.FixedUpdateAsObservable()
			.Where(_ => IsInputEnabled())
			.Select(_ => Input.GetKey(moveRight) || Input.GetKey(altMoveRight))
			.Where(isPressed => isPressed)
			.Subscribe(isPressed => movement = movementBaseSpeed)
			.AddTo(this);

		this.FixedUpdateAsObservable()
			.Where(_ => IsInputEnabled())
			.Select(_ => Input.GetKey(moveLeft) || Input.GetKey(altMoveLeft))
			.Where(isPressed => isPressed)
			.Subscribe(isPressed => movement = (movementBaseSpeed * -1))
			.AddTo(this);

		this.FixedUpdateAsObservable()
			.Where(_ => IsInputEnabled())
			.Select(_ => (!Input.GetKey(moveRight) && !Input.GetKey(moveLeft)) && 
				(!Input.GetKey(altMoveRight) && !Input.GetKey(altMoveLeft)))
			.Where(isLetGo => isLetGo)
			.Subscribe(_ => movement = 0f)
			.AddTo(this);

	}

}
