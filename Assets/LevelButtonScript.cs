using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonScript : MonoBehaviour{

    private Button levelButton;
    private LevelSpawner manager;

    void Start(){
        manager = FindObjectOfType<LevelSpawner>();
        levelButton = transform.GetComponent<Button>();
        levelButton.onClick.AddListener(SaveText);
    }

    private void SaveText() {
        string text = transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text;
        PlayerPrefs.SetString("FolderName", text);
        manager.LoadLevel();
	}
}
