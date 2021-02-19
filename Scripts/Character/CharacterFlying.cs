using UnityEngine;

class CharacterFlying : MonoBehaviour
{

	#region Serialize Fields
	[SerializeField] float jetPackForce;
	[SerializeField] float maxVel;
	[SerializeField] float linearSpeed;

	#endregion

	#region Private Fields


	#endregion

	#region Public Properties


	#endregion
	Rigidbody2D rb;
	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	public void GetHigher()
	{
		Vector2 dir = Vector2.up * Time.deltaTime;
		rb.AddForce(dir * jetPackForce * 100f);

		float clampedY = Mathf.Clamp(rb.velocity.y, -maxVel, maxVel);
		rb.velocity = rb.velocity.ChangeVector(y: clampedY);

	}
	public void LinearMove(float axis)
	{
		float xAxis = axis * Time.deltaTime * linearSpeed;
		rb.velocity = rb.velocity.ChangeVector(x: xAxis);
	}
}