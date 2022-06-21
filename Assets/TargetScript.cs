using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour{

    public bool hasImage;
    public GameObject heldImage;

    void Start(){
        hasImage = false;
        heldImage = null;
    }

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag == "Image") {
            hasImage = false;
            heldImage = null;
		}
	}
}
