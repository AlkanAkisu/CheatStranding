using System.Collections;
using UnityEngine;

class InvokeHandler : MonoBehaviour
{
	public static InvokeHandler i { get; private set; }

	void Awake()
	{
		i = this;
	}

	public void InvokeAction(System.Action action, float seconds)
	{
		StartCoroutine(InvokeActionIEnumerator(action, seconds));
	}
	IEnumerator InvokeActionIEnumerator(System.Action action, float seconds)
	{
		yield return new WaitForSeconds(seconds);
		action();
	}



}