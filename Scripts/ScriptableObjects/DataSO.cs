using UnityEngine;
using NaughtyAttributes;


[CreateAssetMenu(fileName = "DataSO", menuName = "ScriptableObjects/DataSO", order = 0)]
class DataSO : ScriptableObject
{

	#region SerializeFields

	public DataType type;


	private bool isInt => type == DataType.Integer;
	private bool isFloat => type == DataType.Float;
	private bool isBool => type == DataType.Bool;
	private bool isTransform => type == DataType.Transform;
	private bool isVector2 => type == DataType.Vector2;
	private bool isVector3 => type == DataType.Vector3;
	private bool isComponent => type == DataType.Component;

	[SerializeField] bool resetOnEnable = true;
	[SerializeField, ShowIf(EConditionOperator.And, nameof(isInt), nameof(resetOnEnable)), OnValueChanged(nameof(valueChanged))] private int baseIntValue;
	[SerializeField, ShowIf(nameof(isInt)), OnValueChanged(nameof(valueChanged))] private int intValue;
	[SerializeField, ShowIf(EConditionOperator.And, nameof(resetOnEnable), nameof(isFloat)), OnValueChanged(nameof(valueChanged))] private float baseFloatValue;
	[SerializeField, ShowIf(nameof(isFloat)), OnValueChanged(nameof(valueChanged))] private float floatValue;
	[SerializeField, ShowIf(EConditionOperator.And, nameof(resetOnEnable), nameof(isBool)), OnValueChanged(nameof(valueChanged))] private bool baseBoolValue;
	[SerializeField, ShowIf(nameof(isBool)), OnValueChanged(nameof(valueChanged))] private bool boolValue;
	[SerializeField, ShowIf(nameof(isTransform)), OnValueChanged(nameof(valueChanged))] private Transform transformValue;
	[SerializeField, ShowIf(EConditionOperator.And, nameof(resetOnEnable), nameof(isVector2)), OnValueChanged(nameof(valueChanged))] private Vector2 baseVector2Value;
	[SerializeField, ShowIf(nameof(isVector2)), OnValueChanged(nameof(valueChanged))] private Vector2 vector2Value;
	[SerializeField, ShowIf(EConditionOperator.And, nameof(resetOnEnable), nameof(isVector3)), OnValueChanged(nameof(valueChanged))] private Vector3 baseVector3Value;
	[SerializeField, ShowIf(nameof(isVector3)), OnValueChanged(nameof(valueChanged))] private Vector3 vector3Value;
	[SerializeField, ShowIf(nameof(isComponent)), OnValueChanged(nameof(valueChanged))] private Component componentValue;

	#endregion

	#region Props And Events

	void OnEnable()
	{
		if (!resetOnEnable) return;
		intValue = baseIntValue;
		floatValue = baseFloatValue;
		boolValue = baseBoolValue;
		vector2Value = baseVector2Value;
		vector3Value = baseVector3Value;
	}
	public int IntValue
	{
		get => intValue;
		set
		{
			if (intValue != value)
			{
				intValue = value;
				valueChanged();
			}
			else
			{
				intValue = value;

			}
		}
	}
	public float FloatValue
	{
		get => floatValue;
		set
		{

			if (floatValue != value)
			{
				floatValue = value;
				valueChanged();
			}
			else
			{
				floatValue = value;

			}
		}
	}
	public bool BoolValue
	{
		get => boolValue;
		set
		{
			if (boolValue != value)
			{
				boolValue = value;
				valueChanged();
			}
			else
			{
				boolValue = value;

			}
		}
	}
	public Transform TransformValue
	{
		get => transformValue;
		set
		{
			if (transformValue != value)
			{
				transformValue = value;
				valueChanged();
			}
			else
			{
				transformValue = value;

			}
		}
	}
	public Vector2 Vector2Value
	{
		get => vector2Value;
		set
		{
			if (vector2Value != value)
			{
				vector2Value = value;
				valueChanged();
			}
			else
			{
				vector2Value = value;

			}
		}
	}
	public Vector3 Vector3Value
	{
		get => vector3Value;
		set
		{
			if (vector3Value != value)
			{
				vector3Value = value;
				valueChanged();
			}
			else
			{
				vector3Value = value;

			}
		}
	}
	public Component ComponentValue
	{
		get => componentValue;
		set
		{
			if (componentValue != value)
			{
				componentValue = value;
				valueChanged();
			}
			else
			{
				componentValue = value;

			}
		}
	}
	public void valueChanged() => onValueChangedEvent?.Invoke();
	public event System.Action onValueChangedEvent;


	#endregion


	public object GetValue()
	{
		switch (type)
		{
			case DataType.Integer: return intValue;
			case DataType.Float: return floatValue;
			case DataType.Bool: return boolValue;
			case DataType.Transform: return transformValue;
			case DataType.Vector2: return vector2Value;
			case DataType.Vector3: return vector3Value;
			case DataType.Component: return componentValue;
		}
		return null;
	}

	public void SetTransform(Transform transform)
	{
		TransformValue = transform;
		type = DataType.Transform;
	}
	public void SetComponent(Component component)
	{
		ComponentValue = component;
		type = DataType.Component;

	}


}

public enum DataType
{
	Integer,
	Float,
	Bool,
	Transform,
	Vector2,
	Vector3,
	Component
}
