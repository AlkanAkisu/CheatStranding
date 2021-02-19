using System.Linq;
using UnityEditor;
using UnityEngine;


public static class Utils
{
	public static void Log(params object[] objects)
	{
		int N = objects.Length;
		string str = "";
		for (int i = 0; i < N; i++)
		{
			str += objects[i].ToString();
			if (i != N - 1)
				str += " ";
		}
		Debug.Log(str);


	}
	public static Vector2 V2(this Vector3 vect) => vect;


	public static Vector3 ChangeVector(this Vector3 vect, float x = Mathf.Infinity, float y = Mathf.Infinity, float z = Mathf.Infinity)
	{
		return new Vector3(x == Mathf.Infinity ? vect.x : x, y == Mathf.Infinity ? vect.y : y, z == Mathf.Infinity ? vect.z : z);
	}
	public static Vector2 ChangeVector(this Vector2 vect, float x = Mathf.Infinity, float y = Mathf.Infinity)
	{
		return new Vector2(x == Mathf.Infinity ? vect.x : x, y == Mathf.Infinity ? vect.y : y);
	}
	public static bool Between(this float num, float limit) => num == Mathf.Clamp(num, -Mathf.Abs(limit), Mathf.Abs(limit));
	public static bool BetweenTwo(this float num, float upper, float lower) => num == Mathf.Clamp(num, Mathf.Min(lower, upper), Mathf.Max(lower, upper));

	public static float Abs(this float num) => Mathf.Abs(num);
	public static int Sign(this float num) => (int)Mathf.Sign(num);
	public static Color GetColor(string name)
	{
		var getProp = typeof(Color).GetProperty(name);
		if (getProp == null) return Color.green;

		return (Color)getProp?.GetValue(new Color(), null);
	}
	public static bool Up(this KeyCode key) => Input.GetKeyUp(key);
	public static bool Down(this KeyCode key) => Input.GetKeyDown(key);
	public static bool Hold(this KeyCode key) => Input.GetKey(key);

	//***
	//*** Gizmoz
	//***
	public static void DrawArrow(Vector3 from, Vector3 to, int thichness = 1, Color color = default(Color), float halfSize = 0.3f)
	{
		if (color == default(Color)) color = new Color32(0, 1, 0, 1);

		Vector3 perp = Vector2.Perpendicular(to - from).normalized;
		Vector3 vect = (to - from).normalized;


		float h = halfSize * Mathf.Sqrt(3);

		Vector3 headpoint = to;

		Vector3 triangle = headpoint - vect * h;


		DrawLine(triangle - perp * halfSize, triangle + perp * halfSize, thichness * 25, color);
		DrawLine(headpoint, triangle + perp * halfSize, thichness * 25, color);
		DrawLine(triangle - perp * halfSize, headpoint, thichness * 25, color);




		DrawLine(from, to, thichness, color);


	}
	public static void DrawLine(Vector3 from, Vector3 to, int thichness = 1, Color color = default(Color))
	{
		if (color == default(Color)) color = new Color32(0, 1, 0, 1);
		Gizmos.color = color;
		Vector3 perp = Vector2.Perpendicular(to - from);
		Gizmos.DrawLine(from, to);
		for (int i = 1; i < thichness + 1; i++)
		{
			Gizmos.DrawLine(from + perp * 0.0001f * i, to + perp * 0.0001f * i);
			Gizmos.DrawLine(from - perp * 0.0001f * i, to - perp * 0.0001f * i);
		}
	}


	//***
	//*** DEBUG
	//***
	public static void DrawArrowDebug(Vector3 from, Vector3 to, int thichness = 1, Color color = default(Color), float duration = 0, float halfSize = 0.3f)
	{
		if (color == default(Color)) color = new Color32(0, 1, 0, 1);

		Vector3 perp = Vector2.Perpendicular(to - from).normalized;
		Vector3 vect = (to - from).normalized;

		halfSize = 0.3f;
		float h = halfSize * Mathf.Sqrt(3);

		Vector3 headpoint = to;

		Vector3 triangle = headpoint - vect * h;


		DrawLineDebug(triangle - perp * halfSize, triangle + perp * halfSize, thichness * 25, color, duration);
		DrawLineDebug(headpoint, triangle + perp * halfSize, thichness * 25, color, duration);
		DrawLineDebug(triangle - perp * halfSize, headpoint, thichness * 25, color, duration);


		DrawLineDebug(from, to, thichness, color, duration);


	}
	public static void DrawLineDebug(Vector3 from, Vector3 to, int thichness = 1, Color color = default(Color), float duration = 0)
	{
		if (color == default(Color)) color = new Color32(0, 1, 0, 1);

		Vector3 perp = Vector2.Perpendicular(to - from);

		if (duration == 0)
		{
			Debug.DrawLine(from, to, color);
			for (int i = 1; i < thichness + 1; i++)
			{
				Debug.DrawLine(from + perp * 0.0001f * i, to + perp * 0.0001f * i, color);
				Debug.DrawLine(from - perp * 0.0001f * i, to - perp * 0.0001f * i, color);
			}
		}

		else
		{
			Debug.DrawLine(from, to, color, duration);
			for (int i = 1; i < thichness + 1; i++)
			{
				Debug.DrawLine(from + perp * 0.0001f * i, to + perp * 0.0001f * i, color, duration);
				Debug.DrawLine(from - perp * 0.0001f * i, to - perp * 0.0001f * i, color, duration);
			}
		}
	}
	// public static void DrawCone(Vector3 from, Vector3 to, float degree, float radius, bool drawRadius = false)
	// {
	// 	if (drawRadius)
	// 	{

	// 		var vector1 = from + Quaternion.Euler(0, 0, degree / 2) * to * radius;
	// 		var vector2 = from + Quaternion.Euler(0, 0, -degree / 2) * to * radius;

	// 		Gizmos.DrawLine(from, vector1);
	// 		Gizmos.DrawLine(from, vector2);
	// 	}
	// 	Handles.DrawWireArc(from, Vector3.forward, to, degree / 2, radius);
	// 	Handles.DrawWireArc(from, Vector3.forward, to, -degree / 2, radius);
	// }



}
