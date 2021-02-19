using UnityEngine;
using NaughtyAttributes;

class CharacterAmmo : MonoBehaviour
{
	#region Serialize Fields
	[SerializeField, Expandable] DataSO ammoRatio;
	[SerializeField, Expandable] DataSO maxAmmo;
	[SerializeField] float ammoReloadSeconds;


	#endregion

	#region Private Fields
	[SerializeField, ReadOnly] private int currentAmmo;


	#endregion

	#region Public Properties
	public bool IsAmmoRegen { get; private set; }
	public int CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }

	#endregion

	void Awake()
	{
		currentAmmo = maxAmmo.IntValue;
		maxAmmo.onValueChangedEvent += () => currentAmmo = maxAmmo.IntValue;
		setAmmoRatio();
		IsAmmoRegen = false;
	}
	void Update()
	{
		ammoRegen();
		setAmmoRatio();
	}

	private void setAmmoRatio()
	{
		ammoRatio.FloatValue = (float)currentAmmo / (float)maxAmmo.IntValue;
	}
	[Button]
	private void decreaseCurrent()
	{
		currentAmmo = Mathf.Clamp(currentAmmo - 1, 0, maxAmmo.IntValue);
		setAmmoRatio();
	}
	float floatAmmo;
	private void ammoRegen()
	{

		if (currentAmmo == 0)
		{
			IsAmmoRegen = true;
			AmmoBar.i.Reloading = true;
		}

		if (!IsAmmoRegen) return;

		floatAmmo += (Time.deltaTime / ammoReloadSeconds * maxAmmo.IntValue);
		currentAmmo = (int)floatAmmo;
		currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo.IntValue);
		if (currentAmmo >= maxAmmo.IntValue)
		{
			IsAmmoRegen = false;
			AmmoBar.i.Reloading = false;
			floatAmmo = 0f;
		}
	}

}