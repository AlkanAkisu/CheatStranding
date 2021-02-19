using UnityEngine;

class Shield : MonoBehaviour, IDamageable
{
	public void TakeDamage(float damage)
	{
		GetComponentInParent<ShieldEnemy>().TakeDamage(damage);
	}
}