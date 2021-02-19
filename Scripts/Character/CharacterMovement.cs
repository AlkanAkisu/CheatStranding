using UnityEngine;
using NaughtyAttributes;
using System;

class CharacterMovement : MonoBehaviour
{

	#region Serialize Fields
	[SerializeField, Foldout("Movement Parameters")] float moveForce;
	[SerializeField, Foldout("Movement Parameters")] float slowForce;
	[SerializeField, Foldout("Movement Parameters")] float maxSpeed;
	[SerializeField, Foldout("Jump")] float jumpForce;
	[SerializeField, Foldout("Jump"), Range(0f, 1f)] float jumpCutMultiplier;
	[SerializeField, Foldout("Jump"), Range(0f, 1f)] float airSlowSpeedFactor;
	[SerializeField, Foldout("Jump")] float timeDelayGrounded;
	[SerializeField, Foldout("Jump")] float timeDelayButtonPress;
	[SerializeField, Foldout("Jump")] float maxVerticalSpeed;
	[SerializeField, Foldout("Jump")] GameObject skin;
	[SerializeField, Foldout("Ground Checks")] Transform leftGroundCheck;
	[SerializeField, Foldout("Ground Checks")] Transform rightGroundCheck;
	[SerializeField, Foldout("Ground Checks")] LayerMask groundLayer;
	[SerializeField, Foldout("Animation")] Animator anim;



	#endregion

	#region Private Fields

	Rigidbody2D rb;
	[SerializeField, ReadOnly] bool grounded;
	float groundedTimer;
	bool groundedTimerAvailable;
	float btnPressedTimer;
	#endregion

	#region Public Properties
	public float JumpBtnPressed { get; set; }
	public int Direction => transform.localScale.x.Sign();
	public bool Grounded => grounded;
	[ShowNativeProperty] public float Speed => rb != default ? rb.velocity.x : 0f;

	#endregion

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		grounded = true;
		groundedTimerAvailable = true;
		JumpBtnPressed = Mathf.NegativeInfinity;
	}

	void Update()
	{
		checkGround();
		JumpHandler();
		limitVertical();
		PlayAnims();
	}


	#region Move

	private void checkGround()
	{
		var dir = Vector2.down;
		var length = 0.01f;
		var hit1 = Physics2D.Raycast(leftGroundCheck.position, dir, length, groundLayer);
		var hit2 = Physics2D.Raycast(rightGroundCheck.position, dir, length, groundLayer);
		Debug.DrawRay((leftGroundCheck.position + rightGroundCheck.position) / 2f, dir * length);
		if (hit1.transform == null && hit2.transform == null)
		{
			grounded = false;
			if (groundedTimerAvailable)
			{
				groundedTimer = Time.time;
				groundedTimerAvailable = false;
			}
		}
		else
		{
			grounded = true;
			groundedTimer = 0f;
			groundedTimerAvailable = true;
		}
	}
	public void stopCh()
	{
		rb.velocity = rb.velocity.ChangeVector(x: 0);
	}

	bool moving, decc;
	public void Move(float axis)
	{
		Vector2 direction = axis * Time.fixedDeltaTime * Vector2.right;
		Vector2 force = direction * moveForce;


		rb.AddForce(force);
		float clampedX = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
		rb.velocity = new Vector2(clampedX, rb.velocity.y);

		CheckDirection();
		moving = true;
		decc = false;
	}

	public void Decelerate()
	{
		var airParameter = grounded ? 1 : airSlowSpeedFactor;
		var dir = -Vector2.right * rb.velocity.x.Sign() * Time.fixedDeltaTime;
		rb.AddForce(dir * slowForce * rb.velocity.x.Abs() * airParameter);
		moving = false;
		decc = true;
	}

	private void PlayAnims()
	{
		if (!grounded)
		{
			if (rb.velocity.y > 1f) anim.Play("Jump");
			else if (rb.velocity.y < -1f) anim.Play("Fall");
		}
		else
		{
			if (decc)
				anim.Play("Idle");
			else if (moving)
				anim.Play("Walk");
		}
	}
	private void CheckDirection()
	{
		var scale = transform.localScale;
		scale.x = scale.x.Abs() * rb.velocity.x.Sign() == 0 ? 1 : (int)rb.velocity.x.Sign();

		transform.localScale = scale;
	}

	#endregion


	#region Jump

	void JumpHandler()
	{
		HandleJumpScale();
		if (Time.time - JumpBtnPressed >= timeDelayButtonPress) return;

		Jump();

	}

	public void ClimbLadder()
	{
		rb.velocity = rb.velocity.ChangeVector(y: 5);
	}

	public void Jump()
	{
		if (Time.time - groundedTimer < timeDelayGrounded || grounded)
		{
			AudioManager.i.Play("Jump");

			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			groundedTimer = -100f;
			groundedTimerAvailable = false;
			JumpBtnPressed = -timeDelayButtonPress;
		}

	}
	public void CutJumpSpeed()
	{
		if (rb.velocity.y > 0)
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutMultiplier);

	}

	private void limitVertical()
	{
		if (rb.velocity.y > 0) return;
		var vect = rb.velocity;
		vect.y = vect.y.Abs() > maxVerticalSpeed ? maxVerticalSpeed * vect.y.Sign() : vect.y;
		rb.velocity = vect;
	}

	private void HandleJumpScale()
	{
		if (grounded)
		{
			skin.transform.localScale = skin.transform.localScale.ChangeVector(y: 1f);

		}
		else
		{
			var scale = 1 + rb.velocity.y.Sign() * 0.1f;
			skin.transform.localScale = skin.transform.localScale.ChangeVector(y: scale);
		}
	}


	#endregion


}