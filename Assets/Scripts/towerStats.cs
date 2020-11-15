using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class towerStats : MonoBehaviour {

	public int health = 100;
	public int damage = 10;
	public GameObject myTurretGUI;


	void Update() {
		myTurretGUI.GetComponent<Text> ().text = "Health: " + health;


	}
}
