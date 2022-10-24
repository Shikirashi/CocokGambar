using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetScript : MonoBehaviour{

    public bool hasImage;
    public GameObject heldImage;
    public TextMeshProUGUI targetText;
    public Image image;

    void Start(){
        hasImage = false;
        heldImage = null;
        GetComponent<Image>().enabled = true;
    }

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag == "Image") {
            hasImage = false;
            heldImage = null;
            GetComponent<Image>().enabled = true;
        }
	}
}
