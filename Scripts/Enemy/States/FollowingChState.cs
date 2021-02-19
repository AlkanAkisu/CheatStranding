using System;
using UnityEngine;

class FollowingChState : EnemyState
{
	private float invokeTime = 2f, nextTime;
	GunEnemyStatsSO gunStats;
	public new FlyingEnemy enemy => base.enemy as FlyingEnemy;
	public FollowingChState(FlyingEnemy enemy) : base(enemy)
	{
		gunStats = enemy.stats as GunEnemyStatsSO;
	}

	public Vector2 chPos => enemy.chTransform.position;

	float timeToDropOut;

	public override void onEnterState()
	{
		timeToDropOut = Time.time + 15f;
		var speed = enemy.stats.WalkSpeed;
		enemy.Path.Speed = speed;
		followCh();
		nextTime = Time.time + invokeTime;
	}

	public override void onStateUpdate()
	{
		if (Time.time > timeToDropOut)
			enemy.SetState(new PathPatrolState(enemy));


		enemy.Path.followPath();

		if (Time.time > nextTime)
		{
			followCh();
			nextTime = Time.time + invokeTime;
		}

		checkTransition();
		ChangeDir();
	}

	private void ChangeDir()
	{
		enemy.Sp.flipX = enemy.Rb.velocity.x > 0;
	}

	public override void checkTransition()
	{
		var radius = gunStats.GunRange * 0.7f;
		var hits = Physics2D.OverlapCircleAll(enemy.Pos, radius);
		var ch = Array.Find(hits, hit => hit.GetComponent<Character>() != null);
		if (ch != null)
			enemy.SetState(new FireState(enemy));
	}



	public override void onExitState()
	{
		enemy.Rb.velocity = enemy.Rb.velocity.ChangeVector(x: 0);
		enemy.Sp.flipX = false;
	}


	private void followCh() => enemy.Path.startPath(chPos + Vector2.up * 1.5f);
}