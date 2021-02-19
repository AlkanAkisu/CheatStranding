using UnityEngine;
using UnityEngine.UI;

class AmmoBar : MonoBehaviour
{
	[SerializeField, NaughtyAttributes.Expandable] DataSO ammoRatio;
	[SerializeField] DataSO chTransform;
	[SerializeField] RectTransform bar;
	[SerializeField] Vector3 offset;
	private CanvasScaler canvasScaler;
	private Camera _cam;
	private Color color;
	public bool Reloading { get; set; }

	public static AmmoBar i { get; private set; }

	void Awake()
	{
		Reloading = false;
		_cam = Camera.main;
		ammoRatio.onValueChangedEvent += () => bar.localScale = new Vector3(ammoRatio.FloatValue, 1f, 1f);
		canvasScaler = GameObject.FindObjectOfType<CanvasScaler>();
		color = bar.GetComponent<Image>().color;
		i = this;
	}
	void LateUpdate()
	{
		// var screenPoint = _cam.WorldToScreenPoint(chTransform.TransformValue.position);
		// transform.position = screenPoint + offset * Screen.width / canvasScaler.referenceResolution.x;

		if (Reloading)
			bar.GetComponent<Image>().color = Color.yellow;
		else
			bar.GetComponent<Image>().color = color;
	}
}