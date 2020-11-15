using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCam : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.A)) {
			if(transform.position.x > 0) {
			transform.Translate(Vector3.left * Time.deltaTime * 5.0f);
			}
		}
		if(Input.GetKey(KeyCode.D)) {
			
			if(transform.position.x < 10.8) {
			transform.Translate(Vector3.right * Time.deltaTime * 5.0f);
		}
		}
	}
}
