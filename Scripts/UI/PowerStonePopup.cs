using UnityEngine;

class PowerStonePopup : MonoBehaviour
{

	[SerializeField] RectTransform popup;
	private Vector3 defScale;
	private bool firstTime;

	void Awake()
	{
		defScale = popup.transform.localScale;
		popup.transform.localScale = Vector3.zero;
		firstTime = true;
	}
	public void DisablePopup(Transform ch)
	{
		if (ch.GetComponent<Character>() == null) return;
		popup.transform.localScale = Vector3.zero;
	}

	public void ShowPopup(Transform ch)
	{
		if (ch.GetComponent<Character>() == null) return;
		popup.transform.localScale = defScale;
		if (firstTime)
		{
			AudioManager.i.PlayTimed("Cheat Acquired", 0.5f);
			SpawnManager.i.spawnPos = transform.position;
		}
		firstTime = false;

	}
}