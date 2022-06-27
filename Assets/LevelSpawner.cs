using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSpawner : MonoBehaviour{

    public static string[] folders;
    public GameObject Transition;
    public LevelLoader loader;
    public GameObject LevelPanel;
    public GameObject levelButtonPrefab;

    void Start(){
        Subfolder();
        SpawnLevels();
    }

    public static void Subfolder() {
        string path = Application.streamingAssetsPath;
        folders = Directory.GetDirectories(path);

        for (int i = 0; i < folders.Length; i++) {
            folders[i] = folders[i].Remove(0, path.Length + 1);
            Debug.Log(folders[i]);
        }
    }

    public void MainMenu() {
        loader.LoadMainMenu();
    }

    public void SpawnLevels() {
		for (int i = 0; i < folders.Length; i++) {
            GameObject button = Instantiate(levelButtonPrefab, LevelPanel.transform);
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = folders[i];
		}
	}

    public void LoadLevel() {
        loader.LoadLevelScene();
	}
}
