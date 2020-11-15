using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitAI : MonoBehaviour {

	public string currentState = "Walking";

	public Animator anim;
	public bool melee = true;
	public float unitRange = 1f;
	public float moveSpeed = 2f;

	public float attackTimer = 0f;
	public float attackTime = 2f;

	public int hp = 30;
	public int ad = 1;
	public int def = 1;
	public int ap = 1;

	public int worth = 50;
	public GameObject target;
	public GameObject scripts;
	public LayerMask layerMask;


	public GameObject arrow;
	public GameObject spawnPoint;

	public GameObject closestEnemy;

	// Use this for initialization
	void Start () {
		hp = 50;
		if (melee) {
			unitRange = 0.4f;

		}


		if(!melee) {

			spawnPoint = transform.GetChild(0).gameObject;
		}

		anim = GetComponent<Animator>();

		scripts = GameObject.Find("_Scripts");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(!melee) {
		//Debug.Log("Closest en: " + closestEnemy);
		}
		if (string.Equals (currentState, "Walking")) {
			Walking ();
		//	Debug.Log ("WHY AM I WALKING");
		}
		if (string.Equals (currentState, "Waiting")) {
			Waiting ();

		//	Debug.Log ("WAIT");
		}
		if (string.Equals (currentState, "AttackingUnit")) {
			AttackingUnit ();
		//	Debug.Log ("NOW");
		}
		if (string.Equals (currentState, "AttackingTower")) {
			AttackingTower ();
		}


			//Debug.Log ("Hi! " + hit.collider.name + "Me: " + gameObject.tag);
	


			if (string.Equals (gameObject.tag, "Unit")) {  //if friendly unit

			ChooseState("Enemy", "Unit", "EnemyTower");

			}

		else {
			ChooseState("Unit", "Enemy", "Tower");
		}
				
		
	}



	void ChooseState(string type, string enType, string enTower) {
		currentState = "Walking";


		Vector2 direction = Vector2.right;

		if(string.Equals (enType, "Unit")) {
			
			direction = Vector2.right;
		}
		else if(string.Equals (enType, "Enemy")) {
			
			direction = Vector2.left;
		}

		RaycastHit2D hit = Physics2D.Raycast (transform.GetChild(0).gameObject.transform.position, direction, unitRange, layerMask);

			if (hit.collider != null) {

				if (string.Equals (hit.collider.tag, type)) {
					target = hit.collider.gameObject;
					currentState = "AttackingUnit";
				}
				else if (string.Equals (hit.collider.tag, enType)) {
					if(Vector2.Distance(transform.position, hit.collider.transform.position) > 1.5f) {
						//Debug.Log("Dist: " + (Vector2.Distance(transform.position, hit.collider.transform.position)));


						currentState = "Walking";
					}
					else {
			
					if(string.Equals (tag, "Unit")) {

						if(scripts.GetComponent<gameStats>().enemies.Count > 0) {

							closestEnemy = (GameObject)scripts.GetComponent<gameStats>().enemies.Peek();
						//	Debug.Log("Dist: " + Vector2.Distance(closestEnemy.transform.position, transform.position));
							if(Vector2.Distance(closestEnemy.transform.position, transform.position) < unitRange) {
								currentState = "AttackingUnit";
								target = closestEnemy;
							}
							else {
								currentState = "Waiting";
							}
						}
						else {
							currentState = "Waiting";
						}
					}
					else {
						if(scripts.GetComponent<gameStats>().units.Count > 0) {
							closestEnemy = (GameObject)scripts.GetComponent<gameStats>().units.Peek();

							if(Vector2.Distance(closestEnemy.transform.position, transform.position) < unitRange) {
								currentState = "AttackingUnit";
								target = closestEnemy;
							}
							else {
								currentState = "Waiting";
							}

						}
						else {

							currentState = "Waiting";
						}
					}
					}

				} else if (string.Equals (hit.collider.tag, enTower)) {
					currentState = "AttackingTower";

				} else {

				currentState = "Walking";

		
				    
					

				}
			}

	}

	void Walking() {
		attackTimer = 0f;
		anim.SetBool("moving", true);
		anim.SetBool("idle", false);
		anim.SetBool("attacking", false);
		transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
		closestEnemy = null;
		target = null;
	}


	void Waiting() {
		attackTimer = 0f;
		anim.SetBool("idle", true);
		anim.SetBool("moving", false);
		anim.SetBool("attacking", false);
		closestEnemy = null;
		target = null;
	}

	void AttackingUnit() {
		anim.SetBool("attacking", true);
		anim.SetBool("moving", false);
		anim.SetBool("idle", false);

		attackTimer += Time.deltaTime;

		if (attackTimer >= attackTime && target != null) {
			int tgdef = target.GetComponent<unitAI> ().def;

			// 2 * attack * (4 - defence) + (attack pen * attack pen / (4 - def))

			// 3 att 1 pen vs 3 def = 6 + 1 = 7
			//1 att 3 pen vs 3 def = 2 + 9 = 11
			//3 att 1 pen vs 1 def = 18 + 0 = 18
			//1 att 3 pen vs 1 def = 6 + 3 = 9
			//3 att 3 pen vs 3 def = 6 + 9 = 15
			//3 att 3 pen vs 1 def = 18 + 3 = 21
			//1 att 1 pen vs 3 def = 2 + 1 = 3
			//1 att 1 pen vs 1 def = 6 + 0 = 6


			int pt1 = 2 * ad * (4 - tgdef);
			int pt2 = (ap * ap) / (4 - tgdef);
			int pt3 = pt1 + pt2;

			target.GetComponent<unitAI> ().hp -= pt3;

			Debug.Log(gameObject.name + " dealt " + pt3 + " damage, taking enemy " + target.name + " down to " + target.GetComponent<unitAI> ().hp);

			if(target.GetComponent<unitAI> ().hp <= 0) {
				
				if(string.Equals(gameObject.tag, "Unit")) {
				scripts.GetComponent<gameStats>().goldFloat += target.GetComponent<unitAI>().worth;
					scripts.GetComponent<gameStats>().enemies.Dequeue();
				}
				else {
					scripts.GetComponent<gameStats>().enGoldFloat += target.GetComponent<unitAI>().worth;
					scripts.GetComponent<gameStats>().units.Dequeue();
				}



				scripts.GetComponent<gameStats>().WipeTargets(target);
				currentState = "Walking";
			}

			if(!melee) {

				SpawnArrow();

			}

			attackTimer = 0;
		}

	}


	void AttackingTower() {
		anim.SetBool("attacking", true);
		anim.SetBool("moving", false);
		anim.SetBool("idle", false);

	}


	void SpawnArrow() {
		GameObject Arrow = Instantiate(arrow, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;

	}






}
