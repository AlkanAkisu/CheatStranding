using UnityEngine;

class Ladder : MonoBehaviour
{

	[SerializeField] DataSO chTransform;
	Character ch;
	void Start()
	{

	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.transform.GetComponent<Character>() == null) return;
		chTransform.TransformValue.GetComponent<Character>().ClimbingLadder = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.transform.GetComponent<Character>() == null) return;
		chTransform.TransformValue.GetComponent<Character>().ClimbingLadder = false;

	}
}