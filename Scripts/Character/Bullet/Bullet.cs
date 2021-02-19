using UnityEngine;

class Bullet : MonoBehaviour
{
	public float Damage { get; set; }
	[SerializeField] GameObject hitFlying;
	[SerializeField] GameObject hitRush;
	[SerializeField] GameObject hitShield;
	[SerializeField] GameObject hitShieldGuy;
	[SerializeField] GameObject hitRock;
	void OnCollisionEnter2D(Collision2D other)
	{
		var damageable = other.transform.GetComponent<IDamageable>();


		Destroy(gameObject);

		GameObject particleEffect = hitRock;
		var enemy = other.transform.GetComponent<Enemy>();

		if (enemy != null)
		{

			switch (enemy.GetType().Name)
			{
				case nameof(RushingEnemy):
					particleEffect = hitRush;
					break;

				case nameof(ShieldEnemy):

					if (((ShieldEnemy)enemy).ShieldBroke)
						particleEffect = hitShieldGuy;
					else
						particleEffect = hitShield;
					break;

				case nameof(FlyingEnemy):
					particleEffect = hitFlying;
					break;
			}
		}
		try
		{
			var pos = other.contacts[0].point;
			Instantiate(particleEffect, pos, Quaternion.identity);
		}
		catch (System.Exception)
		{

		}



		if (damageable != null)
			damageable.TakeDamage(Damage);
	}
}