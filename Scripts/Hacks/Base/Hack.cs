using UnityEngine;
using System;

[System.Serializable]
abstract class Hack
{
	[SerializeField] float cooldown, effectTime;
	[SerializeField] string name;
	public float Cooldown => cooldown;
	public float EffectTime => effectTime;
	public string Name => name;
	protected float timeExecuted;



	public abstract void Execute();
	public abstract void BackToNormal();

	public float RemainingCooldown()
	{
		if (timeExecuted == 0) return 0;
		return Mathf.Clamp(timeExecuted + Cooldown + effectTime - Time.time, 0, timeExecuted + Cooldown);
	}
	public bool IsReady()
	{
		bool isReady = RemainingCooldown() <= 0f;
		Utils.Log(GetType().Name, "is", isReady ? "" : "not", "ready:");
		return isReady;
	}
}

