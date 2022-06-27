using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour{

	public LevelLoader loader;

	public void PlayGame() {
		loader.LoadLevelSelect();
	}

	public void QuitGame() {
		Application.Quit();
	}

	public void AuthorLink() {
		Application.OpenURL("https://shikirashi.github.io/");
	}
}
