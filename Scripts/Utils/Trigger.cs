using UnityEngine;
using UnityEngine.Events;

class Trigger : MonoBehaviour
{
	[SerializeField] protected TransformEvent onEnterEvent, onExitEvent, onStayEvent;


	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		onEnterEvent?.Invoke(other.transform);
	}
	public virtual void OnTriggerExit2D(Collider2D other)
	{
		onExitEvent?.Invoke(other.transform);
	}
	public virtual void OnTriggerStay2D(Collider2D other)
	{
		onStayEvent?.Invoke(other.transform);
	}
	public virtual void OnCollisionEnter2D(Collision2D other)
	{
		onEnterEvent?.Invoke(other.transform);
	}
	public virtual void OnCollisionStay2D(Collision2D other)
	{
		onExitEvent?.Invoke(other.transform);
	}
	public virtual void OnCollisionExit2D(Collision2D other)
	{
		onStayEvent?.Invoke(other.transform);
	}

}
[System.Serializable]
class TransformEvent : UnityEvent<Transform> { }