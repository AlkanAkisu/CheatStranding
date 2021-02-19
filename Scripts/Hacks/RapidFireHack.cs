using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
class RapidFireHack : Hack
{
	[SerializeField] private DataSO fireDelay;
	[SerializeField] private float factor;
	private float defaultDelay;

	public RapidFireHack()
	{

	}

	public override void Execute()
	{
		timeExecuted = Time.time;
		defaultDelay = fireDelay.FloatValue;
		fireDelay.FloatValue = defaultDelay / factor;
	}
	public override void BackToNormal()
	{
		fireDelay.FloatValue = defaultDelay;
	}
}