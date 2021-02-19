using System;
using UnityEngine;

[System.Serializable]
class KillEveryboyHack : Hack
{
	[SerializeField] DataSO chTr;


	public override void BackToNormal()
	{

	}

	public override void Execute()
	{
		timeExecuted = Time.time;
		var hits = Physics2D.OverlapCircleAll(chTr.TransformValue.position, 15f);
		var enemies = Array.FindAll(hits, hit => hit.CompareTag("Enemy"));
		Array.ForEach(enemies, enemy => enemy.GetComponent<Enemy>().DestroyItself());
	}
}