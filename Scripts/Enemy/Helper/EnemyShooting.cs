using UnityEngine;
using NaughtyAttributes;

class EnemyShooting : Enemy
{
	#region Serialize Fields
	[SerializeField, Foldout("Shooting")] Transform gunPoint;
	[SerializeField, Foldout("Shooting")] GameObject bulletprefab;

	#endregion

	#region Private Fields
	float nextTimeFire;

	#endregion

	#region Public Properties

	public Transform GunPoint => gunPoint;
	public GameObject Bulletprefab => bulletprefab;
	public float BulletDamage => ((GunEnemyStatsSO)stats).BulletDamage;
	public float BulletSpeed => ((GunEnemyStatsSO)stats).BulletSpeed;
	private bool canFire => Time.time >= nextTimeFire;
	private float fireDelay => ((GunEnemyStatsSO)stats).FireDelay;

	#endregion

	public void Shoot(Vector2 dir)
	{
		if (!canFire) return;
		bool left = Vector2.Dot(toCh, Vector2.left).Sign() > 0f;
		var angle = Vector2.Angle(Vector2.down, toCh);
		angle *= left ? 1 : -1;


		var rotation = Quaternion.Euler(0f, 0f, -angle - 90f);
		var bullet = Instantiate(bulletprefab, gunPoint.position, rotation);
		bullet.GetComponent<Bullet>().Damage = BulletDamage;
		Destroy(bullet, 5f);

		var rb = bullet.GetComponent<Rigidbody2D>();
		rb.velocity = BulletSpeed * toCh * Time.deltaTime;
		nextTimeFire = Time.time + fireDelay;

		handleSFX();
	}

	private void handleSFX()
	{
		if (this is FlyingEnemy) AudioManager.i.Play("Flying Enemy Shoot");
		if (this is ShieldEnemy) AudioManager.i.Play("Enemy Shoot");
	}

	public override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		try
		{
			// base.OnDrawGizmos();
			Gizmos.color = Color.white;
			Gizmos.DrawWireSphere(transform.position, ((GunEnemyStatsSO)stats).GunRange);

		}
		catch (System.Exception)
		{


		}

	}

	public Vector2 chPos => chTransform.position;

}