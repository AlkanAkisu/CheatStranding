using UnityEngine;

abstract class EnemyState
{
	protected Enemy enemy;
	public EnemyState(Enemy enemy)
	{
		this.enemy = enemy;
	}

	public abstract void onEnterState();
	public abstract void onStateUpdate();



	public abstract void onExitState();
	public abstract void checkTransition();
}