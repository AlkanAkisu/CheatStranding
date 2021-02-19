using UnityEngine;

abstract class EnemyStatsSO : ScriptableObject
{
	[SerializeField] float visionSightRadius;
	[SerializeField] float seenAngle;
	[SerializeField] float maxHealth;
	[SerializeField] float walkSpeed;

	public float VisionSightRadius { get => visionSightRadius; }
	public float MaxHealth { get => maxHealth; }
	public float WalkSpeed { get => walkSpeed; }
	public float SeenAngle { get => seenAngle; }

}
