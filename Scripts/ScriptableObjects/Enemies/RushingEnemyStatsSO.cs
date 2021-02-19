using UnityEngine;

[CreateAssetMenu(fileName = "RushingEnemyStatsSO", menuName = "ScriptableObjects/RushingEnemyStatsSO", order = 0)]
class RushingEnemyStatsSO : EnemyStatsSO
{
	[SerializeField] float rushSeconds;
	[SerializeField] float baseRushSpeed;
	[SerializeField] float rushDamage;
	[SerializeField] float boomRadius;
	[SerializeField] float boomActivateDistance;
	[SerializeField] float waitBeforeBoom;



	public float RushSeconds => rushSeconds;
	public float RushDamage => rushDamage;
	public float BoomActivateDistance => boomActivateDistance;
	public float BoomRadius => boomRadius;

	public float WaitBeforeBoom => waitBeforeBoom;
	public float BaseRushSpeed => baseRushSpeed;

	
}