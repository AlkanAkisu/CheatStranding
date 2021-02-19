using System.Collections;
using TMPro;
using UnityEngine;

class EndingText : MonoBehaviour
{
	[SerializeField] TMP_Text tmp;
	[SerializeField] RectTransform endingText;
	[SerializeField] RectTransform credits;
	[SerializeField] float speedDelay;
	[SerializeField] float waitTime;
	[SerializeField] float fadeOutSeconds;
	[SerializeField] bool isCredits;

	void Start()
	{
		var txt = tmp.text;
		tmp.text = "";
		StartCoroutine(showText(txt));
	}

	IEnumerator showText(string text)
	{
		text += "";
		int N = text.Length;

		for (int i = 0; i < N; i++)
		{
			tmp.text += text[i];
			yield return new WaitForSeconds(speedDelay);
		}
		var nonRedText = tmp.text;

		if (!isCredits)
		{

			for (int i = 0; i < "MONSTER".Length; i++)
			{
				tmp.text = nonRedText + giveRed("MONSTER".Substring(0, i + 1));
				yield return new WaitForSeconds(speedDelay);
			}

			Utils.Log("Finished");
			yield return new WaitForSeconds(waitTime);
			endingText.gameObject.SetActive(false);
			credits.gameObject.SetActive(true);
		}

	}

	private string giveRed(string str) => "<color=red>" + str + "</color>";
}