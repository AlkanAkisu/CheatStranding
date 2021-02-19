using UnityEngine;
using UnityEngine.SceneManagement;

class MainMenu : MonoBehaviour
{
	public void StartGame()
	{
		SceneManager.LoadScene("Main Scene");
	}
}