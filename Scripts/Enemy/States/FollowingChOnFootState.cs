using System;
using UnityEngine;

class FollowingChOnFootState : EnemyState
{
	public FollowingChOnFootState(Enemy enemy) : base(enemy)
	{
	}

	public override void checkTransition()
	{
		var radius = (enemy.stats as GunEnemyStatsSO).GunRange * 0.7f;
		var hits = Physics2D.OverlapCircleAll(enemy.Pos, radius);
		var ch = Array.Find(hits, hit => hit.GetComponent<Character>() != null);
		if (ch != null)
			enemy.SetState(new FireState(enemy));
	}

	public override void onStateUpdate()
	{
		var dir = Vector2.Dot(enemy.toCh, Vector2.right).Sign();
		enemy.Rb.velocity = enemy.stats.WalkSpeed * dir * Time.deltaTime * Vector2.right;


		checkTransition();
	}

	public override void onEnterState()
	{

	}

	public override void onExitState()
	{
		enemy.Sp.flipX = false;
		enemy.Rb.velocity = enemy.Rb.velocity.ChangeVector(x: 0);
	}
}