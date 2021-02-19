using UnityEngine;

[CreateAssetMenu(fileName = "ShieldEnemyStatsSO", menuName = "ScriptableObjects/ShieldEnemyStatsSO", order = 0)]
class ShieldEnemyStatsSO : GunEnemyStatsSO
{
	[SerializeField] float shieldHealth;
	[SerializeField] float shieldRegen;
	[SerializeField] float shieldRegenDelay;

	public float ShieldHealth => shieldHealth;
	public float ShieldRegen => shieldRegen;
	public float ShieldRegenDelay => shieldRegenDelay;
}
