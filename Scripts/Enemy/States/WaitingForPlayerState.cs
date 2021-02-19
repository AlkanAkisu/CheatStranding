using System;
using UnityEngine;
class WaitingForPlayerState : EnemyState
{
	EnemyStatsSO stats;
	public new RushingEnemy enemy => base.enemy as RushingEnemy;
	public WaitingForPlayerState(RushingEnemy enemy) : base(enemy)
	{
		stats = enemy.stats;
	}
	public override void checkTransition() { }
	public override void onEnterState() { }


	public override void onStateUpdate()
	{
		bool found = false;

		int i = 0;
		while (i < 2)
		{
			i++;
			Vector2 dir;
			if (i == 0)
			{
				dir = Vector2.right;
			}
			else
			{
				dir = Vector2.left;
			}
			var hits = Physics2D.RaycastAll(enemy.Pos, dir, stats.VisionSightRadius);
			var ch = Array.Find(hits, hit => hit.transform.GetComponent<Character>() != null);
			if (ch == default || isCoverOnWay(hits))
			{
				found = true;
				break;
			}

		}



		if (found)
			enemy.SetState(new RushAttackState(enemy));


	}


	private bool isCoverOnWay(RaycastHit2D[] rayHits)
	{
		Action<Transform> drawLine = hit => Debug.DrawLine(enemy.Rb.position, hit.position, Color.red);

		bool coverFound = false;
		Transform cover = null;
		foreach (var hit in rayHits)
		{
			cover = hit.transform;
			coverFound = hit.transform.CompareTag("Ground");
			if (coverFound) break;
		}

		if (coverFound)
			drawLine(cover);

		return coverFound;
	}


	public override void onExitState()
	{

	}
}