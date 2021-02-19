using UnityEngine;
using UnityEngine.EventSystems;

class CheatSheetScreen : MonoBehaviour
{

	[SerializeField] TMPro.TMP_Text placeHolder;
	[SerializeField] TMPro.TMP_Text text;
	[SerializeField] TMPro.TMP_InputField inputField;
	[SerializeField] RectTransform panel;
	private Vector3 defaultScale;

	void Awake()
	{
		defaultScale = panel.localScale;
		panel.localScale = Vector3.zero;
	}
	private void FreezeGame(bool freeze)
	{
		if (freeze)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}

	void Update()
	{
		if (!KeyCode.Tab.Down()) return;

		if (Time.timeScale == 1)
			OpenPanel();
		else
			ClosePanel();
	}

	[NaughtyAttributes.Button]
	public void OpenPanel()
	{
		ClearScreen();
		panel.localScale = defaultScale;
		FreezeGame(true);

		EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
		inputField.OnPointerClick(new PointerEventData(EventSystem.current));

	}
	[NaughtyAttributes.Button]
	public void ClosePanel()
	{
		panel.localScale = Vector3.zero;
		FreezeGame(false);
	}

	public void CheatSheetEntered(dynamic code)
	{
		string cheat = code as string;
		Utils.Log("Cheat code:", cheat);
		bool isValid = HackManager.i.HackByString(cheat);
		Utils.Log(isValid ? "Valid Code" : "Not Valid Code");
		ClosePanel();
		ClearScreen();
	}
	public void ClearScreen(dynamic code)
	{
		placeHolder.text = "";
		inputField.text = "";
	}
	public void ClearScreen()
	{
		placeHolder.text = "";
		inputField.text = "";
	}

}