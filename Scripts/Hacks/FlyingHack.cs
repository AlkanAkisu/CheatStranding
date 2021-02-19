using UnityEngine;

[System.Serializable]
class FlyingHack : Hack
{
	[SerializeField] private DataSO isFlying;


	public override void BackToNormal()
	{
		isFlying.BoolValue = false;
	}

	public override void Execute()
	{
		timeExecuted = Time.time;
		isFlying.BoolValue = true;
	}
}