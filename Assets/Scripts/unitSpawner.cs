using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class unitSpawner : MonoBehaviour {


	public GameObject samurai;
	public GameObject archer;
	public GameObject warrior;
	public GameObject spearman;
	public GameObject viking;
	public GameObject sniper;
	public GameObject dragonknight;
	public GameObject sumo;
	public GameObject javalin;
	public GameObject giant;

	public GameObject spawn;
	public bool startQueue = false;
	public int numberQueued;
	public float timer;
	public float spawnTime = 2f;
	public bool startTiming = false;
	public bool canSpawn = true;

	public Sprite samuraiIcon;
	public Sprite archerIcon;
	public Sprite blackIcon;


	public GameObject icon1;
	public GameObject icon2;
	public GameObject icon3;
	public GameObject icon4;
	public GameObject icon5;

	public List<GameObject> icons = new List<GameObject>();

	public Queue spawns = new Queue();

	public bool started = false;


	void Start() {

		icons.Add(icon1);
		icons.Add(icon2);
		icons.Add(icon3);
		icons.Add(icon4);
		icons.Add(icon5);

	}




	void Update() {

	
		if(started) {
			timer += Time.deltaTime;
		}

			if(timer >= spawnTime) {
			if(spawns.Count > 0) {
				canSpawn = true;


			GameObject unit = (GameObject)spawns.Peek();

			spawns.Dequeue();
			timer = 0;
			}
			}
		}







	//public void SpawnUnit(GameObject type, int cost) {
	//	if(spawns.Count <= 4) {
			
				
				//spawns.Enqueue(type, cost);

	//			this.gameObject.GetComponent<gameStats>().goldFloat -= cost;
	//		started = true;
				//ChangeIcon(archerIcon); 

	//	}

//	}




	public void Spawn(GameObject type, int cost) {
		this.gameObject.GetComponent<gameStats>().goldFloat -= cost;
		GameObject Soldier = Instantiate(type, spawn.transform.position, spawn.transform.rotation) as GameObject;


		float tempWorth = cost * 0.80f;

		Soldier.GetComponent<unitAI>().worth = (int)tempWorth;
	    GetComponent<gameStats>().units.Enqueue(Soldier);


	
	}

	public void ChangeIcon(Sprite icon) {
		started = true;
		icons[spawns.Count - 1].gameObject.GetComponent<Image>().sprite = icon;

	
	}

}
