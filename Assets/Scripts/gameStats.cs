using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameStats : MonoBehaviour {


	public int gold;
	public float goldFloat;
	public int enGold = 250;
	public float enGoldFloat;
	public int miners = 1;
	public int enMiners = 1;
	public int researchPoints;
	public int turretHealth = 100;
	public int enTurretHealth = 100;


	public GameObject goldpturn;
	public GameObject engoldpturn;


	public Queue units = new Queue();
	public Queue enemies = new Queue();


	void Start() {
		goldFloat = (float)gold;
		enGoldFloat = (float)enGold;
	}
	void Update() {

		foreach(Object obj in units) {

	//		Debug.Log("Unit: " + obj.name);

		}
		foreach(Object obj in enemies) {

		//	Debug.Log("Enemy: " + obj.name);

		}

		goldpturn.GetComponent<Text>().text = "Gold: " + gold + " (+" + miners + ")";
		engoldpturn.GetComponent<Text>().text = "Gold: " + enGold + " (+" + enMiners + ")";
	}

	void FixedUpdate() {
		goldFloat += (Time.deltaTime * miners );
		gold = (int)goldFloat;

		enGoldFloat += (Time.deltaTime * enMiners );
		enGold = (int)enGoldFloat;
	}


	public void WipeTargets(GameObject enemy) {

		foreach(GameObject obj in units) {
	//		Debug.Log("Unit: " + obj.name);
			enemies.Dequeue();
			obj.GetComponent<unitAI>().closestEnemy = null;
			obj.GetComponent<unitAI>().target = null;
			Destroy(enemy);
		}

	}
}
