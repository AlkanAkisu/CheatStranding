using System;
using UnityEngine;

class FireState : EnemyState
{
	EnemyShooting shooting;
	public Vector2 chPos => enemy.chTransform.position;
	public Vector2 toCh => (chPos - enemy.Pos).normalized;
	GunEnemyStatsSO gunStats;

	public FireState(Enemy enemy) : base(enemy)
	{
		shooting = enemy as EnemyShooting;
		gunStats = enemy.stats as GunEnemyStatsSO;
	}

	public override void checkTransition()
	{
		var radius = gunStats.GunRange;
		var hits = Physics2D.OverlapCircleAll(enemy.Pos, radius);
		var ch = Array.Find(hits, hit => hit.GetComponent<Character>() != null);


		if (ch != null) return;
		if (enemy is FlyingEnemy) enemy.SetState(new FollowingChState(enemy as FlyingEnemy));
		if (enemy is ShieldEnemy) enemy.SetState(new FollowingChOnFootState(enemy as ShieldEnemy));




	}

	public override void onEnterState()
	{

	}

	public override void onExitState()
	{
		var newX = shooting.transform.localScale.x.Abs();
		shooting.transform.localScale = shooting.transform.localScale.ChangeVector(x: newX);
	}

	public override void onStateUpdate()
	{

		shooting.Shoot(toCh);

		checkTransition();

		enemy.Rb.velocity = Vector2.zero;


	}
}