using UnityEngine;
using TMPro;
using NaughtyAttributes;
using System.Collections;
using UnityEngine.UI;

class Chatbox : MonoBehaviour
{
	[SerializeField, Foldout("Components")] RectTransform chatbox;
	[SerializeField, Foldout("Components")] Image image;
	[SerializeField, Foldout("Components")] RectTransform ammoBar;
	[SerializeField, Foldout("Components")] TMP_Text chatboxText;
	[SerializeField, Foldout("TypeWriterStats")] float speedDelay;
	[SerializeField, Foldout("TypeWriterStats")] float waitTime;
	[SerializeField, Foldout("TypeWriterStats")] float fadeOutSeconds;

	[SerializeField] DataSO canChMoveSO;
	[SerializeField, ResizableTextArea] string text;

	private Vector3 defaultScale, defaultScaleAmmoBar;
	void Awake()
	{
		defaultScale = chatbox.localScale;
		defaultScaleAmmoBar = ammoBar.localScale;
		chatbox.localScale = Vector3.zero;
		ammoBar.localScale = defaultScaleAmmoBar;

	}

	[Button]
	public void OpenChatBox()
	{
		chatbox.localScale = defaultScale;
		ammoBar.localScale = Vector3.zero;
	}

	[Button]
	public void CloseChatBox()
	{
		chatbox.localScale = Vector3.zero;
		ammoBar.localScale = defaultScaleAmmoBar;
	}

	public void SetText(string text, bool canChMove, float seconds)
	{
		waitTime = seconds;
		canChMoveSO.BoolValue = canChMove;
		OpenChatBox();
		StartCoroutine(showText(text));
	}
	public void SetText(string text)
	{
		SetText(text, true, 8f);
	}
	public void SetCanChMove(bool canChMove)
	{
		canChMoveSO.BoolValue = canChMove;
	}
	public void SetWaitTime(float waitTime)
	{
		this.waitTime = waitTime;
	}


	// [Button] private void DebugSetText() => SetText(text, seconds: waitTime);

	IEnumerator showText(string text)
	{
		text += " ";
		int N = text.Length;

		for (int i = 0; i < N; i++)
		{
			string normal = text.Substring(0, i);
			string trans = text.Substring(i, N - i);
			chatboxText.text = normal + giveTransparent(trans);
			yield return new WaitForSeconds(speedDelay);
		}
		Utils.Log("Finished");
		yield return new WaitForSeconds(waitTime);
		float alpha = 1;
		var image = chatbox.GetComponent<Image>();
		while (alpha > 0.3f)
		{
			alpha -= Time.deltaTime / fadeOutSeconds;
			var color = image.color;
			color.a = alpha;
			image.color = color;
			Utils.Log("Alpha", alpha);
			yield return new WaitForSeconds(speedDelay); ;
		}

		CloseChatBox();
		showTextFinished();
	}

	private void showTextFinished()
	{
		canChMoveSO.BoolValue = true;
		var color2 = image.color;
		color2.a = 1;
		image.color = color2;
	}

	private Color ChangeAlpha(Color color, float alpha) => new Color(color.r, color.g, color.b, alpha);

	private string giveTransparent(string str) => "<color=#0000>" + str + "</color>";


}
