using System;
using System.Linq;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;

class HackManager : MonoBehaviour
{

	[SerializeField] RapidFireHack rapidFireHack;
	[SerializeField] FlyingHack flyingHack;
	[SerializeField] KillEveryboyHack killEveryboyHack;
	[SerializeField] SlowDownHack slowDownHack;

	[SerializeField, Foldout("Other")] DataSO chTransform;

	public static HackManager i { get; private set; }


	void Awake()
	{
		i = this;
	}


	//RAPID FIRE
	public bool isRapidFireReady() => rapidFireHack.IsReady();
	[Button]
	public void RapidFire()
	{
		if (!isRapidFireReady())
		{
			Cooldown();
			return;
		}
		chTransform.TransformValue.GetComponent<Character>().RapidFire();

		StartCoroutine(HandleHack(rapidFireHack));
	}

	//FLYING
	public bool isFlyingHackReady() => flyingHack.IsReady();

	[Button]
	public void FlyingHack()
	{
		if (!isFlyingHackReady())
		{
			Cooldown();
			return;
		}
		chTransform.TransformValue.GetComponent<Character>().WingsUp();
		StartCoroutine(HandleHack(flyingHack));
	}


	//KILL EVERYBODY
	public bool isKillEverybodyReady() => killEveryboyHack.IsReady();
	[Button]
	public void KillEverybody()
	{
		if (!isKillEverybodyReady())
		{
			Cooldown();
			return;
		}

		StartCoroutine(HandleHack(killEveryboyHack));
		GameFinished.i.KilledAllEnemy();

	}

	//SLOW DOWN
	public bool isSlowDownReady() => slowDownHack.IsReady();
	public Enemy FindNearestRushEnemy()
	{
		var hits = Physics2D.OverlapCircleAll(chTransform.TransformValue.position, 12f, enemyLayer);
		var rushings = Array.FindAll(hits, hit => hit.GetComponent<RushingEnemy>() != null);
		var closest = rushings.OrderBy(distFromCh).ToArray()[0];
		return closest.GetComponent<Enemy>();

	}
	private float distFromCh(Collider2D tr) => (tr.transform.position - chTransform.TransformValue.position).sqrMagnitude;
	public void SlowDown(Enemy enemy)
	{
		if (!isSlowDownReady())
		{
			Cooldown();
			return;
		}
		slowDownHack.TargetEnemy = enemy;
		StartCoroutine(HandleHack(slowDownHack));
	}


	[Button]
	public void SlowDown()
	{
		var nearest = FindNearestRushEnemy();
		SlowDown(nearest);
	}

	//COOLDOWN

	private void Cooldown()
	{
		chTransform.TransformValue.GetComponent<Character>().Cooldown();
	}
	[SerializeField] LayerMask enemyLayer;


	IEnumerator HandleHack(Hack hack)
	{
		hack.Execute();
		Utils.Log(hack.GetType().Name, "Hack Executed:");
		var sec = hack.EffectTime;
		yield return new WaitForSeconds(sec);
		Utils.Log(hack.GetType().Name, "Hack Timeout:");
		hack.BackToNormal();
	}

	public bool HackByString(string code)
	{
		code = code.ToLower();
		switch (code)
		{
			case CheatCodes.FlyingHack:
				FlyingHack();
				return true;
			case CheatCodes.KillAllHack:
				KillEverybody();
				return true;
			case CheatCodes.RapidFireHack:
				RapidFire();
				return true;
			case CheatCodes.SlowDownHack:
				SlowDown();
				return true;
			default:
				return false;


		}
	}


}