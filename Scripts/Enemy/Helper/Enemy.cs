using System;
using NaughtyAttributes;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D))]
abstract class Enemy : StateMachine, IDamageable
{
	#region Serialize Fields
	[SerializeField, Expandable] public EnemyStatsSO stats;
	[SerializeField] DataSO chTransformSO;
	[SerializeField, Foldout("Path")] Transform patrolsTransform;
	[SerializeField, Foldout("Path")] EnemyPath _path;
	[SerializeField, Foldout("Ground Detection")] Transform raycast;
	[SerializeField, Foldout("Ground Detection")] LayerMask groundLayer;
	[SerializeField, Foldout("Sprite")] SpriteRenderer sp;


	#endregion

	#region Private Fields
	[SerializeField, ReadOnly] protected float currHealth;
	private Rigidbody2D rb;
	#endregion

	#region Public Properties
	public Transform Raycast => raycast;
	public LayerMask GroundLayer => groundLayer;
	public Rigidbody2D Rb => rb;
	public Transform chTransform => chTransformSO.TransformValue;
	public int CurrentIndex { get; set; }
	public Transform[] Patrols { get; private set; }
	public EnemyPath Path => _path;
	public Vector2 Pos => transform.position;
	public Vector2 toCh => (chTransform.position.V2() - transform.position.V2()).normalized;
	public Vector2 toChNonNormalized => (chTransform.position.V2() - transform.position.V2());
	public SpriteRenderer Sp => sp;

	#endregion

	protected virtual void Awake()
	{
		currHealth = stats.MaxHealth;
		rb = GetComponent<Rigidbody2D>();
		initPatrols();
	}
	private void initPatrols()
	{

		if (patrolsTransform == null) return;
		Patrols = new Transform[patrolsTransform.childCount];

		for (int i = 0; i < patrolsTransform.childCount; i++)
		{
			Patrols[i] = patrolsTransform.GetChild(i);
		}

	}





	public virtual void TakeDamage(float damage)
	{
		currHealth = Mathf.Clamp(currHealth - damage, 0f, stats.MaxHealth);
		checkDeath();
	}

	public virtual void checkDeath()
	{
		if (currHealth > 0f) return;
		DeadSFX();
		Destroy(gameObject);
	}
	public void DeadSFX() => AudioManager.i.Play("Enemy Death");
	public void DestroyItself()
	{
		DeadSFX();
		Destroy(gameObject);
	}

	public virtual void OnDrawGizmos()
	{
		try
		{
			drawSeeRadius();

			if (patrolsTransform == null) return;

			if (Patrols == null)
				initPatrols();



			Gizmos.color = Color.white;
			Vector3 to, from = transform.position;
			if (Application.isPlaying)
			{
				to = rb.velocity.normalized;
			}
			else
			{

				to = (Patrols[1].position - Patrols[0].position).normalized;
			}



			Gizmos.color = Color.red;


			var length = Patrols.Length;
			Color[] colors = { Color.red, Color.blue, Color.magenta, Color.green, Color.yellow };
			for (int i = 0; i < length; i++)
			{
				Color color;
				if (length == 2 && i == 1)
				{
					from = Patrols[i].position;
					to = Patrols[(i + 1) % length].position;
					Vector3 perp = Vector2.Perpendicular(to - from).normalized;
					float diff = 0.3f;
					color = colors[i % colors.Length];
					Utils.DrawArrow(from + perp * diff, to + perp * diff, 6, color, halfSize: 0.1f);

					break;
				}
				from = Patrols[i].position;
				to = Patrols[(i + 1) % length].position;
				color = colors[i % colors.Length];
				Utils.DrawArrow(from, to, 6, color, halfSize: 0.1f);

			}
		}
		catch
		{

		}




	}
	private void drawSeeRadius()
	{
		var radius = stats.VisionSightRadius;
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(transform.position, radius);
	}


}