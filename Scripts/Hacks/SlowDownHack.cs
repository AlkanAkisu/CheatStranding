using UnityEngine;

[System.Serializable]
class SlowDownHack : Hack
{
	[SerializeField] float slowWalkSpeed;
	private EnemyStatsSO stats;
	private float defWalkSpeed;

	public Enemy TargetEnemy { get; set; }

	public RushingEnemy rushing => TargetEnemy as RushingEnemy;



	public override void BackToNormal()
	{
		if (stats is RushingEnemyStatsSO)
		{
			var rushStats = stats as RushingEnemyStatsSO;
			rushing.ChangeSpeed(slowWalkSpeed);
		}
		else
		{
			rushing.ChangeSpeed(defWalkSpeed);
		}
	}

	public override void Execute()
	{
		stats = TargetEnemy.stats;
		(TargetEnemy as RushingEnemy).Slowed();
		if (stats is RushingEnemyStatsSO)
		{
			var rushStats = stats as RushingEnemyStatsSO;
			defWalkSpeed = rushStats.BaseRushSpeed;
			rushing.ChangeSpeed(slowWalkSpeed);

		}

		timeExecuted = Time.time;
	}
}