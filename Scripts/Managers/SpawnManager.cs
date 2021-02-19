using UnityEngine;

class SpawnManager : MonoBehaviour
{
	public Vector2 spawnPos { get; set; }

	[SerializeField] Transform initialTransform;
	[SerializeField] DataSO chTransform;
	public static SpawnManager i { get; private set; }
	void Awake()
	{
		i = this;
		spawnPos = initialTransform.position;
	}

	public void Spawn()
	{
		chTransform.TransformValue.position = spawnPos;
	}



}