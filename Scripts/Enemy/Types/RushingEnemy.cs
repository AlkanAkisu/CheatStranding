using System.Collections;
using NaughtyAttributes;
using UnityEngine;

class RushingEnemy : Enemy
{
	[SerializeField] Animator _anim;
	[SerializeField] GameObject slowingFX;
	[SerializeField] GameObject trailFx;
	public new RushingEnemyStatsSO stats => (RushingEnemyStatsSO)stats;
	public Animator anim => _anim;

	private float rushSpeed;
	public float RushSpeed => rushSpeed;
	protected override void Awake()
	{
		base.Awake();

	}
	void Start()
	{
		SetState(new WaitingForPlayerState(this));
		rushSpeed = stats.BaseRushSpeed;
	}

	public void Update()
	{
		State?.onStateUpdate();
		var newX = -transform.localScale.x.Abs() * Rb.velocity.x.Sign();
		transform.localScale = transform.localScale.ChangeVector(x: newX);
		TrailHandler();
	}

	[Button] private void Rush() => SetState(new RushAttackState(this));

	void OnCollisionEnter2D(Collision2D other)
	{
		var ch = other.transform.GetComponent<Character>();
		if (ch == null) return;

		ch.TakeDamage(100f);
		SetState(new WaitingForPlayerState(this));

	}

	public void ChangeSpeed(float newSpeed)
	{
		rushSpeed = newSpeed;
	}

	public void Slowed()
	{
		slowingFX.SetActive(true);
	}

	private void TrailHandler()
	{

		if ((State.GetType() != typeof(RushAttackState)))
		{
			trailFx.SetActive(false);
			return;
		}
		if (rushSpeed != stats.BaseRushSpeed)
		{
			trailFx.SetActive(false);
			return;
		}



		trailFx.SetActive(true);
		if (Rb.velocity.x > 0)
			trailFx.transform.localScale = trailFx.transform.localScale.ChangeVector(x: -1);
		else
			trailFx.transform.localScale = trailFx.transform.localScale.ChangeVector(x: 1);
	}

	public override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		try
		{
			// Utils.DrawArrow(transform.position, chTransform.position, color: Color.magenta, halfSize: 0.1f);
			// Utils.Log("Distance", (transform.position - chTransform.position).magnitude);
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, stats.BoomRadius);
		}
		catch (System.Exception)
		{

			throw;
		}

	}
	bool rushingToWall = false;
	void OnCollisionStay2D(Collision2D other)
	{
		if (!other.transform.CompareTag("Ground")) return;
		rushingToWall = true;
	}
	void OnCollisionExit2D(Collision2D other)
	{
		if (!other.transform.CompareTag("Ground")) return;
		rushingToWall = false;

	}

	public override void TakeDamage(float damage)
	{
		if (State.GetType() == typeof(WaitingForPlayerState))
			Rush();
		base.TakeDamage(damage);
	}
}