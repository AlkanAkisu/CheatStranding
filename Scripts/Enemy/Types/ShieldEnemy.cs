using System;
using NaughtyAttributes;
using UnityEngine;


class ShieldEnemy : EnemyShooting
{

	#region Serialize Fields
	[SerializeField, Foldout("Gun")] SpriteRenderer spGun;
	[SerializeField, Foldout("Gun")] Transform gunTransform;
	[SerializeField, Foldout("Shield"), Required] Transform shield;
	[SerializeField] GameObject teardownParticle;

	#endregion

	#region Private Fields
	[SerializeField, ReadOnly] private float _health;
	private bool shieldBroke;
	#endregion

	#region Public Properties

	public new ShieldEnemyStatsSO stats => stats as ShieldEnemyStatsSO;
	public float ShieldHealth { get => _health; set => _health = value; }
	public float ShieldRegenAmount => stats.ShieldRegen;
	public float ShieldRegenDelay => stats.ShieldRegenDelay;
	public SpriteRenderer spShield => shield.GetComponent<SpriteRenderer>();

	public bool ShieldBroke => shieldBroke;

	#endregion

	void Start()
	{
		shieldBroke = false;
		ShieldHealth = stats.ShieldHealth;
		setPatrolState();

	}

	public void Update()
	{
		State?.onStateUpdate();
		if (State.GetType() == typeof(FireState))
			turnGun();

		ShieldRegen();
		HandleDirection();
	}



	private void turnGun()
	{
		var flip = Vector2.Dot(toCh, Vector2.right).Sign() < 0;
		spGun.flipY = flip;

		var angle = Vector2.Angle(Vector2.right, toCh);

		gunTransform.rotation = Quaternion.Euler(0f, 0f, angle);



	}

	public override void TakeDamage(float damage)
	{
		if (shieldBroke)
		{
			base.TakeDamage(damage);
			return;
		}
		if (State.GetType() == typeof(PatrolState))
			setFollowState();

		ShieldDamaged(damage);
	}

	private void ShieldDamaged(float damage)
	{
		ShieldHealth = Mathf.Clamp(ShieldHealth - damage, 0f, stats.ShieldHealth);
		HandleOpacity();
		checkIfBroken();
	}
	private void ShieldRegen()
	{
		var regen = ShieldRegenAmount * Time.deltaTime;
		ShieldHealth = Mathf.Clamp(ShieldHealth + regen, 0f, stats.ShieldHealth);

		HandleOpacity();
		checkIfBroken();
	}

	private void HandleOpacity()
	{
		var t = ShieldHealth / stats.ShieldHealth;
		var alpha = Mathf.Lerp(0.2f, 1f, t);
		var color = spShield.color;
		color.a = alpha;
		spShield.color = color;
	}

	private void checkIfBroken()
	{
		if (ShieldHealth > 0f) return;

		AudioManager.i.Play("Shield Break");

		shieldBroke = true;
		Instantiate(teardownParticle, shield.transform.position, Quaternion.identity);
		shield.gameObject.SetActive(false);

	}

	private void HandleDirection()
	{
		if (Rb.velocity.x < 0)
			transform.localScale = transform.localScale.ChangeVector(x: -1);
		else
			transform.localScale = transform.localScale.ChangeVector(x: 1);
	}

	[Button] private void setPatrolState() => SetState(new PatrolState(this));
	[Button] private void setFollowState() => SetState(new FollowingChOnFootState(this));
	[Button] private void setFireState() => SetState(new FireState(this));

}