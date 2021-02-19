using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class GameFinished : MonoBehaviour
{

	[SerializeField] DataSO isGameFinished;
	[SerializeField] Image fadeOutImage;
	[SerializeField] private float fadeOutSeconds;

	public static GameFinished i { get; private set; }
	void Awake()
	{
		i = this;
	}

	[NaughtyAttributes.Button]
	public void KilledAllEnemy()
	{
		isGameFinished.BoolValue = true;
		StartCoroutine(fadeOut());

	}

	IEnumerator fadeOut()
	{
		float alpha = 0f;
		while (alpha < 0.95f)
		{
			alpha += Time.deltaTime / fadeOutSeconds;
			var color = fadeOutImage.color;
			color.a = alpha;
			fadeOutImage.color = color;
			Utils.Log("Alpha", alpha);
			yield return null;
		}
		SceneManager.LoadScene("Ending");
	}


}