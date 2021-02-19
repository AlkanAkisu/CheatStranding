using System;
using UnityEngine;

class PathPatrolState : EnemyState
{

	public new FlyingEnemy enemy => base.enemy as FlyingEnemy;

	public PathPatrolState(FlyingEnemy enemy) : base(enemy)
	{
		enemy.Path.onPathFinished += onPathFinished;
	}

	public override void checkTransition()
	{
		var hits = Physics2D.OverlapCircleAll(enemy.Pos, enemy.stats.VisionSightRadius);
		var ch = Array.Find(hits, hit => hit.GetComponent<Character>() != null);
		if (ch != null && isLookingAtCh())
			enemy.SetState(new FollowingChState(enemy));

	}

	private bool isLookingAtCh()
	{
		bool enemyRight = Vector2.Dot(enemy.toCh, Vector2.right).Sign() > 0;
		bool spriteRight = !enemy.Sp.flipX;
		return enemyRight == spriteRight;
	}

	public void changePatrol()
	{

		enemy.CurrentIndex = (enemy.CurrentIndex + 1) % enemy.Patrols.Length;
		enemy.Path.startPath(enemy.Patrols[enemy.CurrentIndex].position);
		enemy.Path.Speed = enemy.stats.WalkSpeed;


		var vec = (enemy.Patrols[enemy.CurrentIndex].position.V2() - enemy.Pos).normalized;

	}


	public override void onEnterState()
	{
		var speed = enemy.stats.WalkSpeed;
		enemy.Path.Speed = speed;
		enemy.Path.startPath(enemy.Patrols[enemy.CurrentIndex].position);
	}
	public void onPathFinished()
	{
		//todo sprite flipx here
		changePatrol();
	}

	private void ChangeDir()
	{
		enemy.Sp.flipX = enemy.Rb.velocity.x < 0;
	}

	public override void onStateUpdate()
	{
		enemy.Path.followPath();
		checkTransition();
		ChangeDir();
		checkTransition();
	}

	public override void onExitState()
	{
		enemy.Sp.flipX = false;
		enemy.Rb.velocity = Vector2.zero;
	}
}