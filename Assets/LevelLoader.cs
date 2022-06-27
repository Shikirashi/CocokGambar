using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour{

	public Animator transition;
	public float transitionTime = 1f;

	private void Start() {
		transition = transform.GetComponent<Animator>();
	}

	public void LoadNextLevel(){
		StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
		//FindObjectOfType<AudioManager>().Play("confirmButton");
	}

	public void ReloadLevel() {
		StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
		//FindObjectOfType<AudioManager>().Play("confirmButton");
	}

	public void LoadPreviousLevel() {
		if (transition.gameObject.activeSelf) {
			StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
			//FindObjectOfType<AudioManager>().Play("backButton");
		}
		else {
			Debug.Log("Transition is inactive!");
		}
	}

	public void LoadLevelSelect() {
		StartCoroutine(LoadLevel(1));
	}

	public void LoadMainMenu() {
		StartCoroutine(LoadLevel(0));
	}

	public void LoadLevelScene() {
		StartCoroutine(LoadLevel(2));
	}

	IEnumerator LoadLevel(int levelIndex) {
		//play anim
		transition.SetTrigger("Action");

		//wait
		yield return new WaitForSeconds(transitionTime);

		//load scene
		SceneManager.LoadScene(levelIndex);

	}

}
