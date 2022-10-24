 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragNDrop : MonoBehaviour{

    public float minDist;
    public int itemIndex;
    public GameObject target;
    public GameManager manager;

    void Start(){
        manager = FindObjectOfType<GameManager>();
        minDist = manager.minDist;
        itemIndex = transform.GetSiblingIndex();
    }

    void Update(){
        
    }

	public void ItemDrag() {
        transform.position = Input.mousePosition;
    }

    public void ItemEndDrag() {
        target = FindClosestTarget();
		if (!target.GetComponent<TargetScript>().hasImage) {
            float dist = Vector3.Distance(transform.position, target.transform.position);
            transform.position = Input.mousePosition;

            if (dist < minDist) {
                transform.position = target.transform.position;
                target.GetComponent<TargetScript>().hasImage = true;
                target.GetComponent<TargetScript>().heldImage = this.gameObject;
                target.GetComponent<Image>().enabled = false;
            }
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log(collision.gameObject.name + "hit");
        //collision.transform.position = gameObject.transform.position;
    //}
    public GameObject FindClosestTarget() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Target");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        //Debug.Log("Closest target is " + closest.name);
        return closest;
    }

}
