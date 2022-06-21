using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour{
    public Image itemImage;
    public GameObject correct;
    public GameObject wrong;

    string folderPath;
    string[] filePaths;

    int itemImageIndex;

    private void Awake() {
        itemImage = GetComponent<Image>();
        //Designation for which file is to be loaded
        itemImageIndex = transform.GetSiblingIndex();

        //LoadImage();

        correct = transform.GetChild(0).gameObject;
        wrong = transform.GetChild(1).gameObject;

        correct.SetActive(false);
        wrong.SetActive(false);
    }

    void LoadImage() {
        //Create an array of file paths from which to choose
        folderPath = Application.streamingAssetsPath;  //Get path of folder
        filePaths = Directory.GetFiles(folderPath, "*.jpg"); // Get all files of type .png in this folder

        //Converts desired path into byte array
        byte[] pngBytes = File.ReadAllBytes(filePaths[itemImageIndex]);

        //Creates texture and loads byte array data to create image
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(pngBytes);

        //Creates a new Sprite based on the Texture2D
        Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        //Assigns the UI sprite
        itemImage.sprite = fromTex;
    }
}
