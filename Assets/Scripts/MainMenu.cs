using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void MenuScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void ScoreScreen()
    {
        SceneManager.LoadScene(2);
    }

	public void Quit() {
		Application.Quit();
	}
}
