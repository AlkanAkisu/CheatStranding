using UnityEngine;

[CreateAssetMenu(fileName = "GunEnemyStatsSO", menuName = "ScriptableObjects/GunEnemyStatsSO", order = 0)]
class GunEnemyStatsSO : EnemyStatsSO
{
	[SerializeField] float bulletDamage;
	[SerializeField] float bulletSpeed;
	[SerializeField] float fireDelay;
	[SerializeField] float gunRange;

	public float BulletDamage => bulletDamage;
	public float BulletSpeed => bulletSpeed;
	public float FireDelay => fireDelay;
	public float GunRange => gunRange;

}