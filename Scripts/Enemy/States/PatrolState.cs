using System;
using UnityEngine;

class PatrolState : EnemyState
{

	public PatrolState(Enemy enemy) : base(enemy)
	{

	}

	public override void onEnterState()
	{

	}
	public override void checkTransition()
	{
		var radius = enemy.stats.VisionSightRadius;
		var hits = Physics2D.OverlapCircleAll(enemy.Pos, radius);
		var ch = Array.Find(hits, hit => hit.GetComponent<Character>() != null);

		if (ch == null) return;
		if ((enemy as RushingEnemy) != null) enemy.SetState(new RushAttackState(enemy));

		if (!isLookingAtCh()) return;
		if ((enemy as ShieldEnemy) != null) enemy.SetState(new FireState(enemy));
	}

	private bool isLookingAtCh()
	{
		bool enemyRight = Vector2.Dot(enemy.toCh, Vector2.right).Sign() > 0;
		bool spriteRight = enemy.transform.localScale.x.Sign() > 0;
		return enemyRight == spriteRight;
	}
	public override void onStateUpdate()
	{
		var dir = enemy.transform.localScale.x.Sign();
		enemy.Rb.velocity = enemy.transform.right * enemy.stats.WalkSpeed * Time.deltaTime * dir;
		checkRaycast();
		checkTransition();
	}
	private void checkRaycast()
	{
		Vector2 origin = enemy.Raycast.position;
		Vector2 right = enemy.transform.right;
		Vector2 down = -enemy.transform.up;

		Physics2D.Raycast(origin, right, 0.1f);
		var rightRaycast = Physics2D.Raycast(origin, right, 0.1f, enemy.GroundLayer);
		var downRaycast = Physics2D.Raycast(origin, down, 0.5f, enemy.GroundLayer);

		if (downRaycast == default || rightRaycast != default)
			changeDir();

	}

	private void changeDir()
	{
		enemy.Rb.velocity = -enemy.Rb.velocity;
		// var val = -enemy.transform.localScale.x;
		// enemy.transform.localScale = enemy.transform.localScale.ChangeVector(x: val);
	}


	public override void onExitState()
	{
		var val = enemy.transform.localScale.x.Abs();
		enemy.transform.localScale = enemy.transform.localScale.ChangeVector(x: val);
	}


}