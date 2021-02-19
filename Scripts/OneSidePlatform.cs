using UnityEngine;

class OneSidePlatform : MonoBehaviour
{
	[SerializeField] PlatformEffector2D effector;
	private LayerMask layerMask;

	void Update()
	{
		var ch = Physics2D.RaycastAll(transform.position, Vector2.up, 0.5f, layerMask);
		if (ch == null) return;
		if (KeyCode.DownArrow.Down() || KeyCode.A.Down())
		{
			effector.rotationalOffset = 180;
			Invoke(nameof(backNormal), 0.75f);
		}
	}
	private void backNormal()
	{
		effector.rotationalOffset = 0;
	}
}