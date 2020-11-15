using UnityEngine;
using System.Collections;

public class arrowMove : MonoBehaviour {

	public float moveSpeed = 5f;
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
	}


	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.tag != "Unit") {
		Destroy (gameObject);
		}
	}




}
