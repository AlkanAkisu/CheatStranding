using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(EnemyPath))]
class FlyingEnemy : EnemyShooting
{

	#region Serialize Fields


	#endregion

	#region Private Fields


	#endregion

	#region Public Properties


	#endregion



	bool start = true;
	void Start()
	{

	}


	public void Update()
	{
		if (start)
		{
			setPatrolState();
			start = false;
		}
		State?.onStateUpdate();
		if (State is FireState)
			lookAtPlayer();
	}

	private void lookAtPlayer()
	{
		var dir = -Vector2.Dot(toCh, Vector2.right).Sign();

		var newX = dir * transform.localScale.x.Abs();
		transform.localScale = transform.localScale.ChangeVector(x: newX);


		Rb.rotation = Vector2.Angle(toCh, Vector2.down) * -dir;

	}

	public override void TakeDamage(float damage)
	{
		if (State.GetType() == typeof(PathPatrolState))
			setFollowState();
		base.TakeDamage(damage);
	}

	[Button] private void setPatrolState() => SetState(new PathPatrolState(this));
	[Button] private void setFollowState() => SetState(new FollowingChState(this));
	[Button] private void setFireState() => SetState(new FireState(this));

}