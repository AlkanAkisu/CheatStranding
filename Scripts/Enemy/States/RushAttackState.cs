using System;
using UnityEngine;

class RushAttackState : EnemyState
{
	public new RushingEnemy enemy => enemy as RushingEnemy;
	private float rushFinishTime;
	private float waitTimer;
	private bool isBoom = false;
	private bool isFinished = false;

	public RushAttackState(RushingEnemy enemy) : base(enemy)
	{
	}

	public override void onEnterState()
	{
		rushFinishTime = Time.time + enemy.stats.RushSeconds;
	}

	public override void onStateUpdate()
	{
		if (isFinished) return;
		if (!isBoom)
			Rush();
		else
			Boom();


	}


	private void Rush()
	{
		enemy.anim.Play("Rushing");
		if (Time.time > rushFinishTime)
		{
			enemy.SetState(new WaitingForPlayerState(enemy));
			return;
		}
		var diff = (enemy.chTransform.position.V2() - enemy.Pos);
		var vect = diff.normalized;
		var dir = Vector2.Dot(vect, Vector2.right).Sign() * Time.deltaTime;


		var stats = enemy.stats;
		var rb = enemy.Rb;



		rb.velocity = rb.velocity.ChangeVector(x: enemy.RushSpeed * dir);


	}
	private void Boom()
	{
		if (Time.time < waitTimer)
		{
			enemy.anim.Play("Idle");


			var rb = enemy.Rb;
			rb.velocity = rb.velocity.ChangeVector(x: 0);

		}
		else
		{

		}

	}

	private void DamageInRadius()
	{
		var hits = Physics2D.OverlapCircleAll(
			enemy.transform.position,
			enemy.stats.BoomRadius
		);

		var ch = Array.Find(hits, hit => hit.GetComponent<Character>() != null);
		if (ch != null)
			ch.transform.GetComponent<Character>().TakeDamage(10f);
	}

	public override void checkTransition()
	{

	}



	public override void onExitState()
	{

	}


}