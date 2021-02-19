using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

class Character : MonoBehaviour, IDamageable
{
	#region Serialize Fields
	[SerializeField] DataSO characterTransform;
	[SerializeField, Foldout("FX")] GameObject flyFX;
	[SerializeField, Foldout("FX")] GameObject rapidFireFX;
	[SerializeField, Foldout("FX")] GameObject cooldownFx;
	[SerializeField, Expandable] DataSO canFly;
	[SerializeField, Expandable] DataSO canChMove;
	[SerializeField] Canvas canvas;


	#endregion

	#region Private Fields
	CharacterMovement movement;
	CharacterFlying flying;
	CharacterFiring firing;

	#endregion

	#region Public Properties
	public int Direction => movement.Direction;
	public bool ClimbingLadder { get; set; }

	#endregion

	void Awake()
	{
		movement = GetComponent<CharacterMovement>();
		flying = GetComponent<CharacterFlying>();
		firing = GetComponent<CharacterFiring>();
		characterTransform.TransformValue = transform;
	}


	void FixedUpdate()
	{
		if (!canChMove.BoolValue)
		{
			movement.stopCh();
			return;
		}
		FlyManager();

		HandleFire();
	}

	void LateUpdate()
	{
		handleCanvas();
	}

	private void FlyManager()
	{
		if (canFly.BoolValue)
		{
			if (movement.Grounded)
			{
				HandleMove();
			}
			else
			{
				HandleHorizontalFly();
			}

			HandleFly();
		}
		else
		{
			HandleMove();
			HandleJump();
		}

	}

	private void HandleHorizontalFly()
	{
		float axis = Input.GetAxis("Horizontal");

		flying.LinearMove(axis);

	}

	private void HandleFly()
	{
		float axis = Input.GetAxis("Vertical");
		if (axis > 0.3f)
		{
			flying.GetHigher();
		}
	}

	private void HandleMove()
	{
		float axis = Input.GetAxis("Horizontal");
		if (axis.Abs() > 0.3f)
		{
			movement.Move(axis);
		}
		else
		{
			movement.Decelerate();
		}
	}


	private void HandleJump()
	{
		var keyDown = KeyCode.Space.Down() || KeyCode.W.Down() || KeyCode.UpArrow.Down();
		var keyUp = KeyCode.Space.Up() || KeyCode.W.Up() || KeyCode.UpArrow.Up();

		if (!ClimbingLadder)
		{

			if (keyDown)
				movement.JumpBtnPressed = Time.time;
			if (keyUp)
				movement.CutJumpSpeed();
		}
		else
		{
			float axis = Input.GetAxis("Vertical");
			if (axis > 0.3f)
			{
				movement.ClimbLadder();
			}

		}
	}

	public void WingsUp()
	{
		flyFX.SetActive(true);
		InvokeHandler.i.InvokeAction(() => flyFX.SetActive(false), 1.5f);
	}
	public void RapidFire()
	{
		rapidFireFX.SetActive(true);
		InvokeHandler.i.InvokeAction(() => rapidFireFX.SetActive(false), 1.5f);
	}
	public void Cooldown()
	{
		cooldownFx.SetActive(true);
		InvokeHandler.i.InvokeAction(() => cooldownFx.SetActive(false), 1.5f);
	}

	private void HandleFire()
	{
		if (Input.GetMouseButtonDown(0)) firing.Fire();
	}

	public void TakeDamage(float damage)
	{
		Invoke(nameof(spawning), 0.1f);
	}

	private void spawning()
	{
		SpawnManager.i.Spawn();
	}

	private void handleCanvas()
	{
		canvas.transform.localScale = transform.localScale;
	}
}