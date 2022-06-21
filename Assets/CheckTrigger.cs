using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrigger : MonoBehaviour{
    void Start(){
        
    }

    void Update(){
        
    }

	private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log(collision.gameObject.name + "hit");
        collision.transform.position = gameObject.transform.position;
	}
}
