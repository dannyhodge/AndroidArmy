using UnityEngine;
using System.Collections;

public class soldierMove : MonoBehaviour {

	public int health;
	public float moveSpeed = 5f;
	public int damage;
	public bool melee = true;
	public bool inCombat = false;
	public bool queuedUp = false;
	public GameObject target;
	public GameObject inFront;
	public GameObject behind;

	public float timer;
	public float attackSpeed = 1f;

	public Animator anim;

	public LayerMask layerMask;

	public bool targetTurret = false;
	public bool inTurretSpot = false;
	public GameObject arrow;
	public GameObject spawnPoint;
	public GameObject tower;
	public GameObject allytower;
	public GameObject ownTower;
    public float archerRange = 2.5f;

	public GameObject[] enemies;

	void Start() {

		anim = GetComponent<Animator>();
		tower = GameObject.Find("entower");
		allytower = GameObject.Find ("tower");
		if (gameObject.tag == "Enemy") {
			ownTower = tower;
		} else {
			ownTower = allytower;
		}
	}

	void FixedUpdate () {
		//Animations

		enemies = GameObject.FindGameObjectsWithTag("Enemy");

		if(enemies.Length > 0) {
			targetTurret = false;
		}
		if(queuedUp) {
			anim.SetBool("idle", true);
			anim.SetBool("moving", false);


		}
		else {
			anim.SetBool("moving", true);
			anim.SetBool("idle", false);

		}

		 if(inCombat) {
			
			anim.SetBool("attacking", true);
			anim.SetBool("moving", false);
			anim.SetBool("idle", false);
		}
		else {
			anim.SetBool("attacking", false);
			timer = 0f;
		}


		if(enemies.Length == 0 && inTurretSpot == true) {
			targetTurret = true;
			moveSpeed = 0f;
		}

	

		if(targetTurret) {
			target = tower;
		}
		if(inTurretSpot) {
			inCombat = true;
		}


		if (melee == false) {
			
			RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector3.right, archerRange, layerMask);
			if (hit.collider != null) {
				if (targetTurret == false) {
					if (hit.collider.gameObject != gameObject) {
						if (hit.collider.gameObject != ownTower.transform.GetChild (1)) {
							//	Time.timeScale = 0f;     //Way of seeing range of raycast
							inCombat = true;
							queuedUp = false;
							target = hit.collider.gameObject;
							Debug.Log ("4");
						}
					}
				}

			
			}
		}


		if(inFront == null && melee == true) {
			queuedUp = false;
		}

		if(inCombat == false && queuedUp == false) {
		transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
		}

		if(inCombat) {
			timer += Time.deltaTime;

			if(timer >= attackSpeed) {
				AttackTarget();
			}
		}
	}

	public void SpawnArrow() {
		GameObject Arrow = Instantiate(arrow, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;

	}

	public void AttackTarget() {
		if(target != null) {
			if(target.tag != "Tower") {
		target.GetComponent<soldierMove>().health -= damage;
			
		
		
		timer = 0;
		
		if(target.GetComponent<soldierMove>().health <= 0) {
			


			if(target.GetComponent<soldierMove>().behind != null) {
			target.GetComponent<soldierMove>().behind.gameObject.GetComponent<soldierMove>().inFront = null;
			}
					Destroy (target);
					target = null;
			        
			if(inTurretSpot == false) {
						
			
			inCombat = false;
					}
					else {
						target = tower;
						inCombat = true;
						targetTurret = true;
					}

				GameObject[] allies = GameObject.FindGameObjectsWithTag("Unit");
				foreach(GameObject ally in allies) {

                        if (ally.GetComponent<soldierMove>().target != null)
                        {
                            if (ally.GetComponent<soldierMove>().target.tag != "Tower")
                            {
                                ally.GetComponent<soldierMove>().target = null;
                                ally.GetComponent<soldierMove>().inCombat = false;
                            }
                        }
				}
			}

			}
			else {
				target.GetComponent<towerStats>().health -= damage;

				timer = 0;
			}

		}
	}



	void OnCollisionEnter2D(Collision2D coll) {




		if(melee) {
		if((gameObject.tag == "Unit" && coll.collider.tag == "Enemy") || (gameObject.tag == "Enemy" && coll.collider.tag == "Unit")) {
				if(targetTurret == false) {
			inCombat = true;
			target = coll.gameObject;
					Debug.Log ("1");
				}
		}
		}
		if((gameObject.tag == "Unit" && coll.collider.tag == "Unit") || (gameObject.tag == "Enemy" && coll.collider.tag == "Enemy")) {
			///if(gameObject.name == "Soldier") {
			if(inCombat == false) {

				if(inFront == null) {


				inFront = coll.gameObject;
					coll.gameObject.GetComponent<soldierMove>().behind = gameObject;
					

				}
			queuedUp = true;
				}
			//}

			if(!melee) {
				if(inCombat == false) {
					
					if(inFront == null) {
						
						
						inFront = coll.gameObject;
						coll.gameObject.GetComponent<soldierMove>().behind = gameObject;
						
						
					}
					if(target == null) {

					queuedUp = true;
					}
				}
			}
		}
		

	}

void OnTriggerEnter2D(Collider2D coll1) {
		if(coll1.tag == "Block") {
			
			//WHEN ALLY HITS ENEMY BLOCK, OR VICE VERSA, TARGET TURREY
			
			if(gameObject.tag == "Unit") {
				if(coll1.name == "Enemy Block") {

					inTurretSpot = true;
					target = coll1.gameObject.transform.parent.gameObject;
					Debug.Log ("2");
					targetTurret = true;
					anim.SetBool("attacking", true);
					inCombat = true;

				
				}
			}

			if(gameObject.tag == "Enemy") {
				if(coll1.name == "Block") {

					inTurretSpot = true;
					target = coll1.gameObject.transform.parent.gameObject;
					Debug.Log ("3");
					targetTurret = true;
					anim.SetBool("attacking", true);
					inCombat = true;


				}
			}
			
			
		}
	}


	void OnCollisionExit2D(Collision2D colly) {

		if((gameObject.tag == "Unit" && colly.collider.tag == "Unit") || (gameObject.tag == "Enemy" && colly.collider.tag == "Enemy")) {

			queuedUp = false;


	     }

	}


}
