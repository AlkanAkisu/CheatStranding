using UnityEngine;
using System.Collections;
using NaughtyAttributes;

class CharacterFiring : MonoBehaviour
{
	#region Serialize Fields
	[SerializeField, Foldout("Bullet")] float bulletSpeed;
	[SerializeField, Foldout("Bullet")] GameObject bulletPrefab;
	[SerializeField, Foldout("Bullet")] float damage;
	[SerializeField, Foldout("Bullet"), Expandable] DataSO fireDelaySO;
	[SerializeField, Foldout("Recoil")] float recoilTime;
	[SerializeField, Foldout("Recoil")] float recoilLength;
	[SerializeField, Foldout("Gun")] Transform gunPoint;
	[SerializeField, Foldout("Gun")] GameObject gun;
	[SerializeField] Animator anim;

	#endregion

	#region Private Fields
	float firedTime;
	private Character character;
	CharacterAmmo ammo;

	#endregion

	#region Public Properties
	public float Damage { get => damage; set => damage = value; }

	#endregion

	void Awake()
	{
		firedTime = Time.time;
		character = GetComponent<Character>();
		ammo = GetComponent<CharacterAmmo>();
	}
	private bool canFire => Time.time - firedTime > fireDelaySO.FloatValue;
	private bool enoughAmmo => !ammo.IsAmmoRegen;
	public void Fire()
	{
		if (!canFire) return;
		if (!enoughAmmo) return;

		ammo.CurrentAmmo--;

		AudioManager.i.Play("Player Shoot");

		var bullet = Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);
		bullet.GetComponent<Bullet>().Damage = Damage;

		Destroy(bullet, 5f);
		anim.Play("Shoot");


		var rb = bullet.GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * bulletSpeed * Time.deltaTime * transform.localScale.x.Sign();
		firedTime = Time.time;
	}


}