using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour{

    public float minDist;
    public GameObject transition;
    public GameObject winPanel;
    public GameObject pausePanel;
    public GameObject errorPanel;
    public GameObject itemParent;
    public GameObject targetParent;
    public LevelLoader loader;

    public GameObject itemPrefab;
    public GameObject targetPrefab;

    public Image[] itemImages;
    public GameObject[] items;
    public GameObject[] targets;
    public List<string> answers = new List<string>();

    public bool[] score;
    private string foldername;

    void Start() {
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
        errorPanel.SetActive(false);
        foldername = PlayerPrefs.GetString("FolderName");
        DirectoryInfo streamingAssets = new DirectoryInfo(Application.streamingAssetsPath + "/" + foldername);
        itemImages = new Image[DirCount(streamingAssets)];

        items = new GameObject[itemImages.Length];
        targets = new GameObject[itemImages.Length];

        ParseJawaban();
        SpawnImages();
    }

    void ParseJawaban() {
        var stream = new StreamReader(Application.streamingAssetsPath + "/" + foldername + "/jawaban.txt");
		while (!stream.EndOfStream) {
            answers.Add(stream.ReadLine());
		}
    }

    public void CheckImages() {
        int nilai = 0;
        score = null;
        score = new bool[items.Length];
		for (int i = 0; i < targets.Length; i++) {
			if (targets[i].GetComponent<TargetScript>().heldImage == null) {
                Debug.LogError("Error, held image on " + targets[i].name + " is null");
                StartCoroutine("ShowError");
                //kamu belum memasukkan semua gambar!
                return;
			}
		}
        for (int i = 0; i < items.Length; i++) {
			if (items[i] == targets[i].GetComponent<TargetScript>().heldImage) {
                score[i] = true; //correct
                Debug.Log("Correct, " + items[i].name + " on " + targets[i].name);
            }
			else {
                score[i] = false; //wrong
                Debug.Log("Wrong, " + items[i].name + " on " + targets[i].name);
            }
		}
		for (int i = 0; i < score.Length; i++) {
			if (score[i] == true) {
                items[i].transform.GetComponent<ImageLoader>().correct.SetActive(true);
                items[i].transform.GetComponent<ImageLoader>().wrong.SetActive(false);
                nilai++;
            }
			else {
                items[i].transform.GetComponent<ImageLoader>().wrong.SetActive(true);
                items[i].transform.GetComponent<ImageLoader>().correct.SetActive(false);
            }
            items[i].transform.GetComponent<DragNDrop>().enabled = false;
		}

        winPanel.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = nilai + "/" + score.Length;
        winPanel.SetActive(true);
	}

    void SpawnImages() {
		for (int i = 0; i < itemImages.Length; i++) {
            GameObject itm = Instantiate(itemPrefab, itemParent.transform);
            itm.name = i.ToString();
            items[i] = itm;

            byte[] pngBytes = File.ReadAllBytes(Application.streamingAssetsPath + "/" + foldername + "/" + (itm.transform.GetSiblingIndex() + 1) + ".jpg");

            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(pngBytes);

            Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

            Image itemImage = itm.GetComponent<Image>();
            itemImage.sprite = fromTex;

            GameObject targ = Instantiate(targetPrefab, targetParent.transform);
            targ.GetComponent<TargetScript>().targetText.text = answers[i];
            targ.name = i.ToString();
            targets[i] = targ;
        }

        Shuffle();
	}

    public void Shuffle() {
        List<int> indexes = new List<int>();
        List<Transform> items = new List<Transform>();
        for (int i = 0; i < targetParent.transform.childCount; ++i) {
            indexes.Add(i);
            items.Add(targetParent.transform.GetChild(i));
        }

        foreach (var item in items) {
            item.SetSiblingIndex(indexes[Random.Range(0, indexes.Count)]);
        }
    }

    public void Retry() {
        Time.timeScale = 1f;
        loader.ReloadLevel();
    }

    public void Menu() {
        Time.timeScale = 1f;
        transition.SetActive(true);
        loader.LoadLevelSelect();
	}

    public void Pause() {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
	}

    public void Resume() {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public static long DirCount(DirectoryInfo d) {
        long i = 0;
        // Add file sizes.
        FileInfo[] fis = d.GetFiles();
        foreach (FileInfo fi in fis) {
            if (fi.Extension.Contains("jpg"))
                i++;
        }
        return i;
    }

    public IEnumerator ShowError() {
        errorPanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        errorPanel.SetActive(false);
	}
    //void load images into items

    //void load answers into targets

}
