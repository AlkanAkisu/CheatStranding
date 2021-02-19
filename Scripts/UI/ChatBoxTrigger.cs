using UnityEngine;
using UnityEngine.Events;

class ChatBoxTrigger : Trigger
{
	[SerializeField] bool oneShot = true;
	bool enterRaised = false, exitRaised = false, stayRaised = false;

	public override void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Character>() == null) return;
		if (oneShot && enterRaised) return;
		enterRaised = true;
		base.OnTriggerEnter2D(other);
	}

	public override void OnTriggerStay2D(Collider2D other)
	{
		if (other.GetComponent<Character>() == null) return;
		if (oneShot && stayRaised) return;
		stayRaised = true;
		base.OnTriggerStay2D(other);
	}

	public override void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<Character>() == null) return;
		if (oneShot && exitRaised) return;
		exitRaised = true;
		base.OnTriggerExit2D(other);
	}

}

