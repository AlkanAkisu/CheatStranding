using UnityEngine;

class CameraFollow : MonoBehaviour
{

	[SerializeField] Transform followThis;

	[SerializeField] float smoothTime;
	[SerializeField] float minY, maxY, minX, maxX;
	Vector3 vel = Vector3.zero;
	[SerializeField] private Vector3 offset;

	void Update()
	{
		var vect = Vector3.zero;
		vect = Vector3.SmoothDamp(transform.position, followThis.position + offset, ref vel, smoothTime);
		vect.y = Mathf.Clamp(vect.y, minY, maxY);
		vect.x = Mathf.Clamp(vect.x, minX, maxX);
		vect.z = -10f;
		transform.position = vect;


	}


	[NaughtyAttributes.Button] private void SetAsMinX() => minX = transform.position.x;
	[NaughtyAttributes.Button] private void SetAsMinY() => minY = transform.position.y;
	[NaughtyAttributes.Button] private void SetAsMaxX() => maxX = transform.position.x;
	[NaughtyAttributes.Button] private void SetAsMaxY() => maxY = transform.position.y;







}